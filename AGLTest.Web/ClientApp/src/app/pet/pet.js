// Models for pet views
export var GenderType;
(function (GenderType) {
    GenderType[GenderType["Male"] = 1] = "Male";
    GenderType[GenderType["Female"] = 2] = "Female";
})(GenderType || (GenderType = {}));
;
export var PetType;
(function (PetType) {
    PetType[PetType["Cat"] = 1] = "Cat";
    PetType[PetType["Dog"] = 2] = "Dog";
    PetType[PetType["Fish"] = 3] = "Fish";
})(PetType || (PetType = {}));
;
(function (PetType) {
    function values() {
        return Object.keys(PetType).filter((type) => isNaN(type) && type !== 'values');
    }
    PetType.values = values;
})(PetType || (PetType = {}));
;
;
//# sourceMappingURL=pet.js.map