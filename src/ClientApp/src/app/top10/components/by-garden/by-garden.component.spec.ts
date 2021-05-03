import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ByGardenComponent } from './by-garden.component';

describe('ByGardenComponent', () => {
  let component: ByGardenComponent;
  let fixture: ComponentFixture<ByGardenComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ByGardenComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ByGardenComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
