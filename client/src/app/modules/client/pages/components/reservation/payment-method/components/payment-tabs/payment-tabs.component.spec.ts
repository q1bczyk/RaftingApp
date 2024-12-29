import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PaymentTabsComponent } from './payment-tabs.component';

describe('PaymentTabsComponent', () => {
  let component: PaymentTabsComponent;
  let fixture: ComponentFixture<PaymentTabsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PaymentTabsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PaymentTabsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
