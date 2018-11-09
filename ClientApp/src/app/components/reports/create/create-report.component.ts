import {Component, OnInit, Inject} from '@angular/core';
import {FormBuilder} from '@angular/forms';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Router} from '@angular/router';
import {Validators} from '@angular/forms';

import {GrowlerService, GrowlerMessageType} from '../../shared/growler/growler.service';

@Component({
  selector: 'create-report',
  templateUrl: './create-report.component.html',
  styleUrls: ['./create-report.component.scss']
})
export class CreateReportComponent {
  public leftICD: Array<any> = [];
  public leftSelected: boolean = false;
  public rightICD: Array<any> = [];
  public rightSelected: boolean = false;

  public generalICD: Array<any> = [];
  public generalICDSelected: any = '';
  public allEyeCodes: Array<any> = [];

  public reportCodes: Array<any> = [];
  public mgmtPlans: Array<any> = [];
  public referrals: Array<any> = [];
  public times: Array<any> = [];

  public pastedData: string = '';

  public isCollapsed = true;

  public baseUrl: string = '';

  // TODO - add xsrf token here
  public httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      //'Authorization': 'my-auth-token'
    })
  };

  public submitted: boolean = false;
  public reportForm = this.fb.group({
    PatientDemographicRaw: [''],
    PatientName: ['', Validators.required],
    Location: ['', Validators.required],
    DRS: ['', Validators.required],
    DOB: ['', Validators.required],
    Gender: ['', Validators.required],
    PatientCode: ['', Validators.required],
    ImageCaptureDateTime: ['', Validators.required],
    RightEyeOther: [''],
    LeftEyeOther: [''],
    ManagementPlanID: [''],
    ReferralEntityID: [''],
    ReferralTimeframeID: [''],
    Comments: [''],
    //UserID: ['123'],
    //User: [''],
  });

  constructor(
    private fb: FormBuilder,
    private http: HttpClient,
    private router: Router,
    private growler: GrowlerService,
    @Inject('BASE_URL') baseUrl: string
  ) {
    this.baseUrl = baseUrl;
  }

  ngOnInit() {
    const self = this;

    // lookup a few items for dropdowns, etc
    // TODO - extract this icd lookup/map into its own model and service
    this.http.get<any[]>(this.baseUrl + 'api/icd10').subscribe(
      result => {
        const icd10s = result;

        for (let item of icd10s) {
          item.active = false;

          if (item.Eye == 2) self.generalICD.push(item);
          if (item.Eye == 1) self.rightICD.push(item);
          if (item.Eye == 0) self.leftICD.push(item);
        }
      },
      error => console.error(error));

    this.http.get<any[]>(this.baseUrl + 'api/reportcodes').subscribe(
      result => {
        self.reportCodes = result;
      },
      error => console.error(error));

    this.http.get<any[]>(this.baseUrl + 'api/recommendationplan').subscribe(
      result => {
        const recs = result;

        for (let item of recs) {
          item.active = false;

          if (item.CodeType == 'plan') self.mgmtPlans.push(item);
          if (item.CodeType == 'timeframe') self.times.push(item);
          if (item.CodeType == 'entity') self.referrals.push(item);
        }
      },
      error => console.error(error));
  }


  // parse the pasted demographic data
  public parse(): void {

    let rawText = this.pastedData;
    if (!rawText) {
      this.growler.growl('Please enter some text before parsing.', GrowlerMessageType.Danger);
      return;
    }

    const keyWords = ['Location:', 'DRS:', 'Birthdate:', 'Gender:', 'Code:', 'Image ID:', 'Date:', 'Eye:'];
    let firstInd, nextInd, item = null;

    for (let i = 0; i < keyWords.length; i++) {
      const wrd = keyWords[i];
      firstInd = rawText.indexOf(": ");
      nextInd = rawText.indexOf(wrd, firstInd + 1);

      if (firstInd > -1 && nextInd > -1) {
        item = rawText.substring(firstInd + 1, nextInd);

        // here, assign item to its place on page; unless 'Image ID:' (so really, 'Date:')
        if (wrd != 'Date:') {
          switch (wrd) {
            case 'Location:':
              this.reportForm.controls['PatientName'].setValue(item.trim());
              break;
            case 'DRS:':
              this.reportForm.controls['Location'].setValue(item.trim());
              break;
            case 'Birthdate:':
              this.reportForm.controls['DRS'].setValue(item.trim());
              break;
            case 'Gender:':
              this.reportForm.controls['DOB'].setValue(item.trim());
              break;
            case 'Code:':
              const g = item.trim();
              item = 'O';
              if (g == 'male') item = 'M'; else if (g == 'female') item = 'F';
              this.reportForm.controls['Gender'].setValue(item);
              break;
            case 'Image ID:':
              this.reportForm.controls['PatientCode'].setValue(item.trim());
              break;
            case 'Eye:':
              this.reportForm.controls['ImageCaptureDateTime'].setValue(item.trim());
              break;
          }

        }
        rawText = rawText.substring(nextInd);
      }
    }
  }


  // clicking on an eye ICD diagnosis box; can only select one
  public toggleGeneralICDActive(item): void {
    var savedState = !item.active;
    for (let d of this.generalICD) {
      d.active = false;
    }
    item.active = savedState;

    // toggling a general diagnosis to active, store the item and update selected mgmt plan
    this.generalICDSelected = savedState ? item : '';

    // if unclicking, then clear out management plans; if clicking one, also need to clear out as they get reset below
    this.clearManagementPlans();

    // if there is a general diagnosis being selected, need to clear out right/left eye options
    if (this.generalICDSelected) {
      this.clearRightAssessment();
      this.clearLeftAssessment();

      // also update the management plan, referral, and timeframe options accordingly
      if (item.Code == 'ZZ01') {
        this.mgmtPlans[0].active = true;
        this.referrals[0].active = true;
        this.times[3].active = true;
      } else {
        this.mgmtPlans[1].active = true;
        this.referrals[1].active = true;
        this.times[4].active = true;
      }
    }
  }

  // clicking left or right eye ICD box; can have mult selections
  public toggleRightICDActive(item): void {
    item.active = !item.active;
    this.rightSelected = item.active;
  }

  public toggleLeftICDActive(item): void {
    item.active = !item.active;
    this.leftSelected = item.active;
  }


  // clear all functionality on the eye diagnosis elements
  public clearGeneral(): void {
    for (let d of this.generalICD) {
      d.active = false;
    }
    this.generalICDSelected = '';
  }

  public clearRightAssessment(): void {
    for (let d of this.rightICD) {
      d.active = false;
    }
    this.rightSelected = false;
    this.reportForm.controls['RightEyeOther'].setValue('');
  }

  public clearLeftAssessment(): void {
    for (let d of this.leftICD) {
      d.active = false;
    }
    this.leftSelected = false;
    this.reportForm.controls['LeftEyeOther'].setValue('');
  }

  // choosing a management plan
  public chooseManagementPlan(item): void {
    var savedState = !item.active;
    for (let d of this.mgmtPlans) {
      d.active = false;
    }
    item.active = savedState;
  }

  public chooseReferralPlan(item): void {
    var savedState = !item.active;
    for (let d of this.referrals) {
      d.active = false;
    }
    item.active = savedState;
  }

  public chooseTimeframePlan(item): void {
    var savedState = !item.active;
    for (let d of this.times) {
      d.active = false;
    }
    item.active = savedState;
  }


  // clearing out mgmt plans and referral options
  public clearManagementPlans(): void {
    for (let d of this.mgmtPlans) {
      d.active = false;
    }
    for (let d of this.referrals) {
      d.active = false;
    }
    for (let d of this.times) {
      d.active = false;
    }

  }

  public onSubmit() {
    this.submitted = true;

    // handle eye codes and if referral plans have been selected and if all demog fields have value
    if (!this.eyeCodesCheck() || !this.referralPlansCheck() || !this.demographicFormCheck()) {
      this.growler.growl('Please double-check all required fields.', GrowlerMessageType.Danger);
      return;
    }

    let body = this.reportForm.value;
    this.assignDemographicDataField();
    body['PatientDemographicRaw'] = this.pastedData;

    const self = this;
    return this.http.post<any>(
      this.baseUrl + 'api/diagnosticreport',
      JSON.stringify({
        DiagnosticReport: body,
        ReportCodes: this.allEyeCodes
      }),
      self.httpOptions)
      .subscribe(
        id => {
          // growler shows for 3000 ms
          self.growler.growl('Report saved!', GrowlerMessageType.Success);
          // delay route
          window.setTimeout(() => {
            this.router.navigate(['/reports/view-detail/' + id + '/1']);
          }, 3050);
        },
        error => {
          self.growler.growl('There was an issue saving your report', GrowlerMessageType.Danger);
          console.log(error);
        });
  }

  // be sure there is at least one eyeball code selected
  public eyeCodesCheck(): boolean {
    this.allEyeCodes = [];
    let ICDs = this.generalICD.concat(this.rightICD).concat(this.leftICD);
    for (let item of ICDs) {
      if (item.active) {
        this.allEyeCodes.push({ICD10ID: item.ID});
      }
    }

    return this.allEyeCodes.length ? true : false;
  }

  // be sure there is at least one mgmt plan, ref entity, and ref timeline selected
  public referralPlansCheck(): boolean {
    let plansOk = false;
    for (let d of this.mgmtPlans) {
      if (d.active) {
        this.reportForm.controls['ManagementPlanID'].setValue(d.ID);
        plansOk = true;
      }
    }

    let referralsOk = false;
    for (let d of this.referrals) {
      if (d.active) {
        this.reportForm.controls['ReferralEntityID'].setValue(d.ID);
        referralsOk = true;
      }
    }

    let timesOk = false;
    for (let d of this.times) {
      if (d.active) {
        this.reportForm.controls['ReferralTimeframeID'].setValue(d.ID);
        timesOk = true;
      }
    }
    return (plansOk && timesOk && referralsOk);
  }

  public demographicFormCheck(): boolean {

    // mark any demographic field items as touched and validator will take care of if requred
    let formOk = true;
    Object.keys(this.reportForm.controls).forEach(key => {
      this.reportForm.get(key).markAsTouched();
      const v = this.reportForm.get(key).value;
      if (v == '' && key != 'Comments' && key != 'RightEyeOther' && key != 'LeftEyeOther' && key != 'PatientDemographicRaw') {
        formOk = false;
        return;
      }
    });

    return formOk;
  }

  public assignDemographicDataField(): void {
    // if raw demographic field is empty, need to give it values since it's the only field used for searches
    if (!this.pastedData) {
      this.pastedData = this.reportForm.get('PatientName').value + " "
        + this.reportForm.get('Location').value + " "
        + this.reportForm.get('DRS').value + " "
        + this.reportForm.get('Gender').value + " "
        + this.reportForm.get('PatientName').value;
    }

  }

}

