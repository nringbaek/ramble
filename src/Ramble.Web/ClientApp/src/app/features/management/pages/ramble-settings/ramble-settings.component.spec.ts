import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RambleSettingsComponent } from './ramble-settings.component';

describe('RambleSettingsComponent', () => {
  let component: RambleSettingsComponent;
  let fixture: ComponentFixture<RambleSettingsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RambleSettingsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RambleSettingsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
