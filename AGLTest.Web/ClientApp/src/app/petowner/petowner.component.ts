import { Component, Inject } from '@angular/core'; 
import { HttpClient } from '@angular/common/http';
import { HttpParams } from '@angular/common/http';
import { PetType } from '../pet/pet';
import { Person } from '../pet/pet';

// Component to access pet owners, and pets

@Component({
  selector: 'app-petowner',
  styleUrls: ['../pet/pet.css'],
  templateUrl: './petowner.component.html'
})

export class PetOwnerComponent {

  public people: Person[];
  public petName: string;
  public selectedPetType: string;
  public PetType = PetType;
          
  constructor(private httpClient: HttpClient, @Inject('BASE_URL') private baseUrl: string)
  { }

  search() {
    let params = new HttpParams();

    if (this.petName) {
      params = params.set('petName', this.petName);
    }

    if (this.selectedPetType) {
      params = params.set('petType', this.selectedPetType);
    }

    this.httpClient.get(this.baseUrl + '/api/pet/ownersearch', { params: params }).subscribe((res) => {
      this.people = res as Person[];
    });
  }
}
