import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SelectedEquipmentComponent } from './selected-equipment.component';

describe('SelectedEquipmentComponent', () => {
  let component: SelectedEquipmentComponent;
  let fixture: ComponentFixture<SelectedEquipmentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SelectedEquipmentComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SelectedEquipmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
