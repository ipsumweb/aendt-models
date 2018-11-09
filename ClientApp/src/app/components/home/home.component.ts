import {Component, OnInit, Inject} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {ActivatedRoute} from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent {
  public showSearchResults = false;
  public showAllRecords = false;

  public searchString = '';
  public searchResults: Array<any> = [];

  public baseUrl = '';

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  ngOnInit() {
    const limit = true;
    this.getAllReports(limit);
  }

  public runSearch(): void {
    this.showSearchResults = false;
    this.showAllRecords = false;

    if (this.searchString.length) {
      const self = this;
      this.http.get<any[]>(this.baseUrl + 'api/diagnosticreport/search/' + this.searchString).subscribe(
        result => {
          self.searchResults = result;
          this.showSearchResults = true;
        },
        error => console.error(error));
    }
  }

  public getAllReports(limit = false): void {
    this.searchString = '';
    this.showSearchResults = false;
    this.showAllRecords = true;

    const self = this;

    if (limit) {
      // only get top 20 most recent
      this.http.get<any[]>(this.baseUrl + 'api/diagnosticreport/recent').subscribe(
        result => {
          self.searchResults = result;
          this.showSearchResults = true;
        },
        error => console.error(error));
    } else {
      // get all
      this.http.get<any[]>(this.baseUrl + 'api/diagnosticreport').subscribe(
        result => {
          self.searchResults = result;
          this.showSearchResults = true;
        },
        error => console.error(error));
    }

  }

}
