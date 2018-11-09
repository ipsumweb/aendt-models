import {Component, OnInit, Inject} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {ActivatedRoute} from '@angular/router';
import {Router} from '@angular/router';
import {MatDialog, MatDialogConfig} from "@angular/material";
import html2canvas from 'html2canvas';
import {GrowlerService, GrowlerMessageType} from '../../shared/growler/growler.service';
import {LoadingDialogComponent} from "../../shared/loading-dialog/loading-dialog.component";

@Component({
  selector: 'app-view-detail',
  templateUrl: './view-detail.component.html',
  styleUrls: ['./view-detail.component.scss']
})
export class ViewDetailComponent implements OnInit {
  // @params
  public reportID: number = 0;
  public ifSendFax: boolean = false;

  public baseUrl: string = '';
  public dataLoaded: boolean = false;

  public report: Array<any> = [];
  public referringPlan: string = '';
  public referringTo: string = '';
  public referringTimeline: string = '';

  public reportCodes: Array<any> = [];
  public leftICD: Array<any> = [];
  public rightICD: Array<any> = [];
  public generalICD: string = '';

  public reviewer: Array<any> = [];

  public sendingFax: boolean = false;

  constructor(
    private http: HttpClient,
    private route: ActivatedRoute,
    private router: Router,
    private growler: GrowlerService,
    private dialog: MatDialog,
    @Inject('BASE_URL') baseUrl: string
  ) {
    this.baseUrl = baseUrl;
  }

  ngOnInit() {
    // get route params
    this.route.params.subscribe(params => {
      this.reportID = params['id'];
      this.ifSendFax = params['fax'];
    });

    if (this.ifSendFax) {
      this.openDialog();
    }

    const self = this;

    this.http.get<any[]>(this.baseUrl + 'api/diagnosticreport/' + this.reportID).subscribe(
      result => {
        self.report = result;

        // TODO - these should all be pulled in initial diagnostic report info in C# class; why is it not being pulled?
        this.http.get<any[]>(this.baseUrl + 'api/recommendationplan/' + self.report["ManagementPlanID"]).subscribe(
          result => {
            this.referringPlan = result["Description"];
          },
          error => console.error(error));

        this.http.get<any[]>(this.baseUrl + 'api/recommendationplan/' + self.report["ReferralTimeframeID"]).subscribe(
          result => {
            this.referringTimeline = result["Description"];
          },
          error => console.error(error));

        this.http.get<any[]>(this.baseUrl + 'api/recommendationplan/' + self.report["ReferralEntityID"]).subscribe(
          result => {
            this.referringTo = result["Description"];
          },
          error => console.error(error));

        // also get user info
        this.http.get<any[]>(this.baseUrl + 'api/user/' + self.report["UserID"]).subscribe(
          result => {
            self.reviewer = result;
            self.reviewer["signatureURL"] = "/assets/images/" + self.reviewer["LDAPID"] + ".png";

          },
          error => console.error(error));

        self.dataLoaded = true;
      },
      error => console.error(error));

    this.http.get<any[]>(this.baseUrl + 'api/reportcodes/' + this.reportID).subscribe(
      result => {
        self.reportCodes = result;

        // split out reportCodes into right, left, and general
        // TODO - extract this into its own model and service
        for (let r of result) {
          const item = r["ICD10"];

          if (item.Eye == 2) self.generalICD = item.Summary;
          if (item.Eye == 1) self.rightICD.push(item);
          if (item.Eye == 0) self.leftICD.push(item);
        }

        if (this.ifSendFax) {
          window.setTimeout(() => {
            this.sendFax();
          }, 5000);
        }
      },
      error => console.error(error));
  }


  sendFax(ifSimulateSuccess = 1) {
    this.sendingFax = true;
    const self = this;

    html2canvas(document.querySelector("#capture")).then(canvas => {
      document.body.appendChild(canvas);

      const myCanvas = document.getElementsByTagName('canvas');
      const fullQuality = myCanvas[0].toDataURL('image/jpeg', 1.0);

      return this.http.post<any>(
        this.baseUrl + 'api/fax/' + this.reportID + '/' + ifSimulateSuccess,
        JSON.stringify({
          PageImage: fullQuality
        }))
        .subscribe(
          result => {
            self.growler.growl('Fax successfully queued to send.', GrowlerMessageType.Success);

            self.closeDialog();
            this.sendingFax = false;

          },
          error => console.error(error));
    });
  }


  openDialog() {
    const dialogConfig = new MatDialogConfig();

    dialogConfig.data = {
      id: 1,
      title: 'Angular For Beginners'
    };

    this.dialog.open(LoadingDialogComponent, dialogConfig);

    const dialogRef = this.dialog.open(LoadingDialogComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(
      data => console.log("Dialog output:", data)
    );
  }


  closeDialog() {
    const dialogConfig = new MatDialogConfig();

    dialogConfig.data = {
      id: 1,
      title: 'Angular For Beginners'
    };

    this.dialog.closeAll();

  }

}
