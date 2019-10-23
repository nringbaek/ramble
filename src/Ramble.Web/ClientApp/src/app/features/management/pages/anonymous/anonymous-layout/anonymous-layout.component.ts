import { Component, OnInit } from '@angular/core';
import { fadeUpAnimation } from 'src/app/shared/animations';

@Component({
  selector: 'app-anonymous-layout',
  templateUrl: './anonymous-layout.component.html',
  styleUrls: ['./anonymous-layout.component.scss'],
  animations: [fadeUpAnimation]
})
export class AnonymousLayoutComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }
}
