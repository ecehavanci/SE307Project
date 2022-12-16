using System;
using System.Collections.Generic;

namespace SE307Project
{
    public class Pet
    {
        private String Name { set; get; }

        public String _Name
        {
            get { return Name; }
            set { Name = _Name; }
        }

        private int Age { set; get; }

        public int _Age
        {
            get { return Age; }
            set { Age = _Age; }
        }

        private String Breed { set; get; }

        public String _Breed
        {
            get { return Breed; }
            set { Breed = _Breed; }
        }

        private AnimalType Species { set; get; }

        public String _Species
        {
            get { return Species.ToString().Replace("Species.", "").Replace("A", " A"); }
            set
            {
                if (_Species == "Dog")
                {
                    Species = AnimalType.Dog;
                }
                else if (_Species == "Cat")
                {
                    Species = AnimalType.Cat;
                }
                else if (_Species == "Bird")
                {
                    Species = AnimalType.Bird;
                }
                else if (_Species == "Hamster")
                {
                    Species = AnimalType.Hamster;
                }
                else if (_Species == "Exotic Animal")
                {
                    Species = AnimalType.ExoticAnimal;
                }
            }
        }

        private List<CareType> CareRoutine { get; }

        public Pet()
        {
            CareRoutine = new List<CareType>();
        }

        public Pet(String name, int age, String breed, AnimalType species)
        {
            Name = name;
            Age = age;
            Breed = breed;
            Species = species;
            CareRoutine = new List<CareType>();
        }


        public void AddCareRoutine(String careType)
        {
            careType = careType.ToUpper();
            if (careType == "BATHE")
            {
                CareRoutine.Add(CareType.Bathe);
            }
            else if (careType == "WALK")
            {
                CareRoutine.Add(CareType.Walk);
            }
            else if (careType == "COMB")
            {
                CareRoutine.Add(CareType.Comb);
            }
            else if (careType == "FEED")
            {
                CareRoutine.Add(CareType.Feed);
            }
            else if (careType == "TAKE TO VET")
            {
                CareRoutine.Add(CareType.TakeToVet);
            }
            else if (careType == "PLAY")
            {
                CareRoutine.Add(CareType.Play);
            }
            else if (careType == "GIVE MEDS")
            {
                CareRoutine.Add(CareType.GiveMeds);
            }
        }

        public void AddCareRoutine(int index)
        {
            if (index == 1)
            {
                CareRoutine.Add(CareType.Bathe);
            }
            else if (index == 2)
            {
                CareRoutine.Add(CareType.Walk);
            }
            else if (index == 3)
            {
                CareRoutine.Add(CareType.Comb);
            }
            else if (index == 4)
            {
                CareRoutine.Add(CareType.Feed);
            }
            else if (index == 5)
            {
                CareRoutine.Add(CareType.TakeToVet);
            }
            else if (index == 6)
            {
                CareRoutine.Add(CareType.Play);
            }
            else if (index == 7)
            {
                CareRoutine.Add(CareType.GiveMeds);
            }
        }
    }

    public enum AnimalType
    {
        Cat,
        Dog,
        Bird,
        Hamster,
        ExoticAnimal
    }

    public enum CareType
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