import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { WallOverviewComponent } from './wall-overview.component';

describe('WallOverviewComponent', () => {
  let component: WallOverviewComponent;
  let fixture: ComponentFixture<WallOverviewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ WallOverviewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(WallOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
