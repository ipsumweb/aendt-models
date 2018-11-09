import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {FaxListComponent} from "./fax-list.component";

@NgModule({
  imports: [
    CommonModule
  ],
  exports: [FaxListComponent],
  declarations: [FaxListComponent]
})
export default class FaxListModule {
}
