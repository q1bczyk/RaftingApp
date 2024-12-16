import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EquipmentSelectionComponent } from './equipment-selection.component';

describe('EquipmentSelectionComponent', () => {
  let component: EquipmentSelectionComponent;
  let fixture: ComponentFixture<EquipmentSelectionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EquipmentSelectionComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EquipmentSelectionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
