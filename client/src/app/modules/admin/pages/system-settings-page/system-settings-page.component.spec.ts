import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SystemSettingsPageComponent } from './system-settings-page.component';

describe('SystemSettingsPageComponent', () => {
  let component: SystemSettingsPageComponent;
  let fixture: ComponentFixture<SystemSettingsPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SystemSettingsPageComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SystemSettingsPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
