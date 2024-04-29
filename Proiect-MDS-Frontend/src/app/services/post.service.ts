import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, catchError, throwError } from 'rxjs';
import { environment } from '../../environments/environments';

@Injectable({
    providedIn: 'root',
  })
export class PostService {
    private apiKey = environment.API_KEY+"/postare/";
    constructor(private http: HttpClient) {}
    public getPosts(): Observable<any> {
        return this.http.get<any>(this.apiKey);
    }
    public getPostsByAge(minAge: any,maxRange:any): Observable<any> {
        if(minAge==null && maxRange==null)
            return this.http.get<any>(this.apiKey+'an/'+1900+"/"+new Date().getFullYear());
        if(minAge==null)
            return this.http.get<any>(this.apiKey+'an/'+1900+"/"+maxRange);
        if(maxRange==null)
            return this.http.get<any>(this.apiKey+'an/'+minAge+"/"+new Date().getFullYear());
        return this.http.get<any>(this.apiKey+'an/'+minAge+"/"+maxRange);
    }
    public getPostsByPrice(minPrice: any,maxPrice:any): Observable<any> {
        if(minPrice==null && maxPrice==null)
            return this.http.get<any>(this.apiKey+'pret/'+0+"/"+100000);
        if(minPrice==null)
            return this.http.get<any>(this.apiKey+'pret/'+0+"/"+maxPrice);
        if(maxPrice==null)
            return this.http.get<any>(this.apiKey+'pret/'+minPrice+"/"+100000);
        return this.http.get<any>(this.apiKey+'pret/'+minPrice+"/"+maxPrice);
    }
    public getPostsByKm(minKm: any,maxKm:any): Observable<any> {
        if(minKm==null && maxKm==null)
            return this.http.get<any>(this.apiKey+'km/'+0+"/"+1000000);  
        if(minKm==null)
            return this.http.get<any>(this.apiKey+'km/'+0+"/"+maxKm);
        if(maxKm==null)
            return this.http.get<any>(this.apiKey+'km/'+minKm+"/"+1000000);
        return this.http.get<any>(this.apiKey+'km/'+minKm+"/"+maxKm);
    }
    public getPostsByBrand(brand: any): Observable<any> {
        return this.http.get<any>(this.apiKey+'firma/'+brand);
    }
    public getPostsByModel(model: any): Observable<any> {
        return this.http.get<any>(this.apiKey+'model/'+model);
    }
}