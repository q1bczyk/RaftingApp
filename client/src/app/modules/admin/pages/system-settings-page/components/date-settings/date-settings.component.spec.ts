import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DateSettingsComponent } from './date-settings.component';

describe('DateSettingsComponent', () => {
  let component: DateSettingsComponent;
  let fixture: ComponentFixture<DateSettingsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DateSettingsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DateSettingsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
