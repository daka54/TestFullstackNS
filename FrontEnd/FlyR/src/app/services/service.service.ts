import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'
})

export class ServiceService {

  constructor(private http: HttpClient) { }

  getJourney(origin:string,destination:string): Observable<any> {
    return this.http.get(`FlightManagement/CalculateJourney?origin=${origin}&destination=${destination}`);
  }

}
