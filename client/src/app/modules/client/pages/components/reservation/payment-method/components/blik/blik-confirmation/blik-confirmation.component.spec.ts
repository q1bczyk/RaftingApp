import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BlikConfirmationComponent } from './blik-confirmation.component';

describe('BlikConfirmationComponent', () => {
  let component: BlikConfirmationComponent;
  let fixture: ComponentFixture<BlikConfirmationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BlikConfirmationComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BlikConfirmationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
