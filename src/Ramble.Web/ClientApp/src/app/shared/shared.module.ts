import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

const SHARED_IMPORTS = [
];

@NgModule({
  declarations: [
    SHARED_IMPORTS
  ],
  imports: [
    CommonModule,
    RouterModule
  ],
  exports: [
    SHARED_IMPORTS,
    CommonModule,
    RouterModule
  ]
})
export class SharedModule { }