import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-counter-component',
  templateUrl: './counter.component.html'
})
export class CounterComponent {
  public currentCount = 0;
  private _http: HttpClient;
  private baseUrl: string;
  public success: string;
  public message?: string;

  public submitValues(value, firstWord, secondWord) {
    if (Number(value) < 1000 && Number(value) > 0 && firstWord.length < 25 && firstWord.length > 0 && secondWord.length < 25 && secondWord.length > 0) {
      this.message = null;
      this._http.post<Iterator>(this.baseUrl + 'api/Iterators', { value: Number(value), firstWord: firstWord, secondWord: secondWord }).subscribe(result => {
        this.success = "successful";
      }, error => console.error(error));
    }
    else {
      this.message = "You inputted a value that is not accepted. Make sure choose an integer between 1-1000 and words less than 25 characters";
    }
  }

  constructor(_http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this._http = _http;
    this.baseUrl = baseUrl;
  }

}

interface Iterator {
  value: number;
  firstWord: string;
  secondWord: string;
}
