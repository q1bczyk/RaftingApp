import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AvailableEquipmentComponent } from './available-equipment.component';

describe('AvailableEquipmentComponent', () => {
  let component: AvailableEquipmentComponent;
  let fixture: ComponentFixture<AvailableEquipmentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AvailableEquipmentComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AvailableEquipmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
