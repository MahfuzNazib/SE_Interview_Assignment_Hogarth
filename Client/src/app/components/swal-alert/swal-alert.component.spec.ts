import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SwalAlertComponent } from './swal-alert.component';

describe('SwalAlertComponent', () => {
  let component: SwalAlertComponent;
  let fixture: ComponentFixture<SwalAlertComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SwalAlertComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SwalAlertComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
