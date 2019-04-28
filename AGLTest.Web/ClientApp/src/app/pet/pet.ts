// Models for pet views

export enum GenderType {
  Male = 1,
  Female = 2
};

export enum PetType {
  Cat = 1,
  Dog = 2,
  Fish = 3
};

export namespace PetType {

  export function values() {
    return Object.keys(PetType).filter(
      (type) => isNaN(<any>type) && type !== 'values'
    );
  }
}

export interface Pet {
  name: string;
  type: string;
};

export interface Person {
  name: string;
  gender: string;
  age: number;
  pets: Pet[];
};
