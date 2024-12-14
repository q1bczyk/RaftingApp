import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReservationFormCardComponent } from './reservation-form-card.component';

describe('ReservationFormCardComponent', () => {
  let component: ReservationFormCardComponent;
  let fixture: ComponentFixture<ReservationFormCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ReservationFormCardComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ReservationFormCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
