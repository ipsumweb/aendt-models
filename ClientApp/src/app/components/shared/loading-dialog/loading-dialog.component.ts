import {Component, Inject, OnInit} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material";

@Component({
  selector: 'app-loading-dialog',
  templateUrl: './loading-dialog.component.html',
  styleUrls: ['./loading-dialog.component.scss']
})
export class LoadingDialogComponent implements OnInit {

  description:string = 'hi';


  constructor(
    private dialogRef: MatDialogRef<LoadingDialogComponent>,
    @Inject(MAT_DIALOG_DATA) data
  ) {
    this.description = data.description;

  }

  ngOnInit() {

  }

  close() {
    this.dialogRef.close();
  }

  save() {
    // this.dialogRef.close(this.form.value);
  }


}
