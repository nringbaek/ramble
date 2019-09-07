import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RambleComponent } from './ramble.component';

describe('RambleComponent', () => {
  let component: RambleComponent;
  let fixture: ComponentFixture<RambleComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RambleComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RambleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
