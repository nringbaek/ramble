import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { AutofocusDirective } from './directives/autofocus.directive';
import { OutsideClickDirective } from './directives/outside-click.directive';
import { ReactiveFormsModule } from '@angular/forms';

const SHARED_IMPORTS = [
  AutofocusDirective,
  OutsideClickDirective
];

@NgModule({
  declarations: [
    SHARED_IMPORTS
  ],
  imports: [
    CommonModule,
    RouterModule,
    ReactiveFormsModule
  ],
  exports: [
    SHARED_IMPORTS,
    CommonModule,
    RouterModule,
    ReactiveFormsModule
  ]
})
export class SharedModule { }
