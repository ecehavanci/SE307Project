using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Serialization;

namespace SE307Project
{
    [XmlRoot("Pet")]
    public class Pet
    {
        public String Name { set; get; }
        public int Age { set; get; }
        public String Breed { set; get; }
        public AnimalType Species { set; get; }

        public String GetSpecies()//get speciecies of pet
        {
            return Species.ToString().Replace("Species.", "").Replace("A", " A");
        }

        public void SetSpecies(String _Species)//choose and set the species of the pet from options
        {
            if (_Species.ToUpper() == "DOG" || _Species == "1")
            {
                Species = AnimalType.Dog;
            }
            else if (_Species.ToUpper() == "CAT" || _Species == "2")
            {
                Species = AnimalType.Cat;
            }
            else if (_Species.ToUpper() == "BIRD" || _Species == "3")
            {
                Species = AnimalType.Bird;
            }
            else if (_Species.ToUpper() == "HAMSTER" || _Species == "4")
            {
                Species = AnimalType.Hamster;
            }
            else if (_Species.ToUpper() == "EXOTIC ANIMAL" || _Species == "5")
            {
                Species = AnimalType.ExoticAnimal;
            }
            else
            {
                Species = AnimalType.Undefined;
                Console.WriteLine(
                    "Species must be dog, cat, bird, exotic animal or hamster. Your input was none of those" +
                    " options so species of pet is set to undefined.");
            }
        }

        public List<CareType> CareRoutine { get; }

        public Pet()
        {
            CareRoutine = new List<CareType>();
            Name = "";
            Age = 0;
            Breed = "";
            Species = AnimalType.Undefined;
        }

        public Pet(String name, int age, String breed, String species)
        {
            Name = name;
            Age = age;
            Breed = breed;
            SetSpecies(species);
            CareRoutine = new List<CareType>();
        }


        public void AddCareRoutine(String careType)//add care routine to a pet from options by care type name
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
            else
            {
                Console.WriteLine("This care routine cannot be added");
            }
        }

        public void AddCareRoutine(int index)//add care routine to a pet from options by care type index
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
            else
            {
                Console.WriteLine("There is no care routine with the given name");
            }
        }

        public void RemoveCareRoutine(int index)//remove a care routine with index from a pet
        {
            try
            {
                CareRoutine.RemoveAt(index);
            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine("There is no care routine at the given index");
            }
        }

        public void RemoveCareRoutine(String careType)//remove a care routine with name from a pet
        {
            careType = careType.ToUpper();
            if (careType == "BATHE")
            {
                CareRoutine.Remove(CareType.Bathe);
            }
            else if (careType == "WALK")
            {
                CareRoutine.Remove(CareType.Walk);
            }
            else if (careType == "COMB")
            {
                CareRoutine.Remove(CareType.Comb);
            }
            else if (careType == "FEED")
            {
                CareRoutine.Remove(CareType.Feed);
            }
            else if (careType == "TAKE TO VET")
            {
                CareRoutine.Remove(CareType.TakeToVet);
            }
            else if (careType == "PLAY")
            {
                CareRoutine.Remove(CareType.Play);
            }
            else if (careType == "GIVE MEDS")
            {
                CareRoutine.Remove(CareType.GiveMeds);
            }
            else
            {
                Console.WriteLine("This care coutine cannot be added");
            }
        }


        public string ListCareRoutine()//list of care routines
        {
            string routine = "";
            foreach (CareType careType in CareRoutine)
            {
                routine += careType + ",\t";
            }

            return routine;
        }

        public override string ToString()//print out the details of a pet
        {
            String petInfo = Name + ": " + "a " + Age + " years old " + Species + "(" + Breed + ")";
            petInfo += "\nCare Routine:\t " + ListCareRoutine() + "\n";

            return petInfo;
        }
    }

    public enum AnimalType //to handle animal types
    {
        Cat,
        Dog,
        Bird,
        Hamster,
        ExoticAnimal,
        Undefined,
    }

    public enum CareType//to handle care types
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