import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { WallsComponent } from './walls.component';

describe('WallsComponent', () => {
  let component: WallsComponent;
  let fixture: ComponentFixture<WallsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ WallsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(WallsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
