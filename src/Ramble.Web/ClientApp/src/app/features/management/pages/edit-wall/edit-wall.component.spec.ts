import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EditWallComponent } from './edit-wall.component';

describe('EditWallComponent', () => {
  let component: EditWallComponent;
  let fixture: ComponentFixture<EditWallComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EditWallComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditWallComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
