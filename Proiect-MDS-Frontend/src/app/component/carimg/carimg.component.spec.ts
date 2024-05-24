import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CarimgComponent } from './carimg.component';

describe('CarimgComponent', () => {
  let component: CarimgComponent;
  let fixture: ComponentFixture<CarimgComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CarimgComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CarimgComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
