using System.Collections.Generic;

namespace SE307Project
{
    public class Pet
    {
        private int Age;
        private string Breed;
        private string Name;
        private List<CareType> CareRoutine;
        private AnimalType Species;

        public Pet()
        {
            CareRoutine = new List<CareType>();
        }
    }

    enum AnimalType    {
        Cat,
        Dog,
        Hamster,
        ExoticAnimal,
        Bird
    }

    enum CareType
    {
        Bathe,
        Walk,
        Comb,
        Feed,
        TakeToVet,
        Play,
        GiveMeds
    }
}