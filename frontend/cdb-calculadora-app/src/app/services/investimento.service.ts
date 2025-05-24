import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

export interface CalcularInvestimentoDto {
  valorInicial: number;
  prazoMeses: number;
}

export interface ResultadoInvestimentoDto {
  valorBruto: number;
  valorLiquido: number;
}

@Injectable({
  providedIn: 'root',
})
export class InvestimentoService {
  private readonly apiUrl = 'http://localhost:5000/api/investimento/calcular';

  constructor(private http: HttpClient) {}

  calcularInvestimento(dto: CalcularInvestimentoDto): Observable<ResultadoInvestimentoDto> {
    return this.http.post<ResultadoInvestimentoDto>(this.apiUrl, dto);
  }
}