import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InvestimentoFormComponent } from './investimento-form.component';

describe('InvestimentoFormComponent', () => {
  let component: InvestimentoFormComponent;
  let fixture: ComponentFixture<InvestimentoFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [InvestimentoFormComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(InvestimentoFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
