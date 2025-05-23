import { Component } from '@angular/core';
import { InvestimentoFormComponent } from './components/investimento-form/investimento-form.component';
import { HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [HttpClientModule, InvestimentoFormComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'cdb-calculadora-app';
}
