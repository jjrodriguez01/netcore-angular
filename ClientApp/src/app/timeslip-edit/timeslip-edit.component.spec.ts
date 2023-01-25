import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TimeslipEditComponent } from './timeslip-edit.component';

describe('TimeslipEditComponent', () => {
  let component: TimeslipEditComponent;
  let fixture: ComponentFixture<TimeslipEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TimeslipEditComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TimeslipEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
