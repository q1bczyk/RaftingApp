import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConfrimAccountComponent } from './confrim-account.component';

describe('ConfrimAccountComponent', () => {
  let component: ConfrimAccountComponent;
  let fixture: ComponentFixture<ConfrimAccountComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ConfrimAccountComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ConfrimAccountComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
