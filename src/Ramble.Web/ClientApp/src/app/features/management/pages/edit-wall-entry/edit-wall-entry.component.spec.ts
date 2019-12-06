import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EditWallEntryComponent } from './edit-wall-entry.component';

describe('EditWallEntryComponent', () => {
  let component: EditWallEntryComponent;
  let fixture: ComponentFixture<EditWallEntryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EditWallEntryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditWallEntryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
