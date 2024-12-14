import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StepsItemComponent } from './steps-item.component';

describe('StepsItemComponent', () => {
  let component: StepsItemComponent;
  let fixture: ComponentFixture<StepsItemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [StepsItemComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(StepsItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
