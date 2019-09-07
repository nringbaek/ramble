import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewRamblesComponent } from './view-rambles.component';

describe('ViewRamblesComponent', () => {
  let component: ViewRamblesComponent;
  let fixture: ComponentFixture<ViewRamblesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ViewRamblesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ViewRamblesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
