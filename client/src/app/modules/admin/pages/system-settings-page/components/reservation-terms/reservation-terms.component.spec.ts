import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReservationTermsComponent } from './reservation-terms.component';

describe('ReservationTermsComponent', () => {
  let component: ReservationTermsComponent;
  let fixture: ComponentFixture<ReservationTermsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ReservationTermsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ReservationTermsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
