import { CommonModule, CurrencyPipe } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { InvestimentoService, CalcularInvestimentoDto, ResultadoInvestimentoDto } from 'src/app/services/investimento.service';

@Component({
  selector: 'app-investimento-form',
  standalone: true,
  templateUrl: './investimento-form.component.html',
  styleUrls: ['./investimento-form.component.css'],
  imports: [
    CommonModule,          // ngIf, currency pipe
    FormsModule,           // ngModel, ngForm
    HttpClientModule       // HttpClient usado pelo serviço
  ],
  providers: [CurrencyPipe]
})
export class InvestimentoFormComponent {
  model: CalcularInvestimentoDto = {
    valorInicial: 0,
    prazoMeses: 0,
  };

  resultado: ResultadoInvestimentoDto | null = null;
  error: string | null = null;

  constructor(private investimentoService: InvestimentoService) {}

  calcular() {
    this.resultado = null;
    this.error = null;

    this.investimentoService.calcularInvestimento(this.model).subscribe({
      next: (res: ResultadoInvestimentoDto) => this.resultado = res,
      error: (err: any) => {
        if (err.error?.erros) {
          this.error = err.error.erros.map((e: any) => `${e.Campo}: ${e.Erro}`).join(', ');
        } else {
          this.error = 'Erro inesperado.';
        }
      }
    });
  }
}
