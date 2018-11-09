import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import FaxListModule from "../../shared/fax-list/fax-list.module";
import {FaxListComponent} from "../../shared/fax-list/fax-list.component";
import {MatDialogModule} from "@angular/material";
import {LoadingDialogComponent} from "../../shared/loading-dialog/loading-dialog.component";

@NgModule({
  imports: [
    CommonModule,
    FaxListModule,
    MatDialogModule
  ],
  entryComponents: [
    FaxListComponent,
    LoadingDialogComponent
  ],
  providers: [
    MatDialogModule
  ]
})
export class ViewDetailModule {
}
