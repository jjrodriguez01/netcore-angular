import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TimeslipComponent } from './timeslip.component';

describe('TimeslipComponent', () => {
  let component: TimeslipComponent;
  let fixture: ComponentFixture<TimeslipComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TimeslipComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TimeslipComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
