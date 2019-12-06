import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-left-menu',
  templateUrl: './left-menu.component.html',
  styleUrls: ['./left-menu.component.scss']
})
export class LeftMenuComponent implements OnInit {
  isMaximized = true;

  constructor() { }

  ngOnInit() {
  }

  toggleMinMaximize() {
    this.isMaximized = !this.isMaximized;
  }
}
