import { Injectable } from '@angular/core';
import {environment} from '../../environments/environments';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
@Injectable({
    providedIn: 'root'
})
export class OpenaiService {
    private apiKey = environment.API_KEY+"/openai/";
    constructor(private httpClient: HttpClient){
    }
    response(prompt:any):Observable<any>{
        return this.httpClient.post<any>(this.apiKey+'getdescription',prompt);
    }
    customSearch(prompt:any):Observable<any>{
        return this.httpClient.post<any>(this.apiKey+'getCars',prompt);
    }
}