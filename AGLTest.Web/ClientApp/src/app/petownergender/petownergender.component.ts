import { Component, Inject } from '@angular/core'; 
import { HttpClient } from '@angular/common/http';
import { HttpParams } from '@angular/common/http';
import { PetType } from '../pet/pet';

// Component for accessing pets by owner gender

@Component({
  selector: 'app-petownergender',
  styleUrls: ['../pet/pet.css'],
  templateUrl: './petownergender.component.html'
})

export class PetOwnerGenderComponent {

  public PetType = PetType;  
  public genders: {};               // pets are stored as an associative array of owner gender
  public selectedPetType: string;   // selected pet type used in data filter
          
  constructor(private httpClient: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  search() {
    let params = new HttpParams();

    if (this.selectedPetType) {
      params = params.set("petType", this.selectedPetType);
    }

    this.httpClient.get(this.baseUrl + '/api/pet/ownergendersearch', { params: params }).subscribe((res) => {
      this.genders = res;
    });
  }
}
