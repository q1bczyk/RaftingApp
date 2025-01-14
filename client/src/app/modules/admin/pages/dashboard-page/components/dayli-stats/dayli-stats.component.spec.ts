import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DayliStatsComponent } from './dayli-stats.component';

describe('DayliStatsComponent', () => {
  let component: DayliStatsComponent;
  let fixture: ComponentFixture<DayliStatsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DayliStatsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DayliStatsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
