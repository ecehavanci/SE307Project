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

        public String GetSpecies() //get speciecies of pet
        {
            return Species.ToString().Replace("Species.", "").Replace("A", " A");
        }

        public void SetSpecies(String _Species) //choose and set the species of the pet from options
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


        public void AddCareRoutine(String careType) //add care routine to a pet from options by care type name
        {
            careType = careType.ToUpper();
            if (careType == "BATHE")
            {
                if (!CareRoutine.Contains(CareType.Bathe))
                {
                    CareRoutine.Add(CareType.Bathe);
                }
                else
                {
                    Console.WriteLine("Already added.");
                }
            }
            else if (careType == "WALK")
            {
                if (!CareRoutine.Contains(CareType.Walk))
                {
                    CareRoutine.Add(CareType.Walk);
                }
                else
                {
                    Console.WriteLine("Already added.");
                }
            }
            else if (careType == "COMB")
            {
                if (!CareRoutine.Contains(CareType.Comb))
                {
                    CareRoutine.Add(CareType.Comb);
                }
                else
                {
                    Console.WriteLine("Already added.");
                }
            }
            else if (careType == "FEED")
            {
                if (!CareRoutine.Contains(CareType.Feed))
                {
                    CareRoutine.Add(CareType.Feed);
                }
                else
                {
                    Console.WriteLine("Already added.");
                }
            }
            else if (careType == "TAKE TO VET")
            {
                if (!CareRoutine.Contains(CareType.TakeToVet))
                {
                    CareRoutine.Add(CareType.TakeToVet);
                }
                else
                {
                    Console.WriteLine("Already added.");
                }
            }
            else if (careType == "PLAY")
            {
                if (!CareRoutine.Contains(CareType.Play))
                {
                    CareRoutine.Add(CareType.Play);
                }
                else
                {
                    Console.WriteLine("Already added.");
                }
            }
            else if (careType == "GIVE MEDS")
            {
                if (!CareRoutine.Contains(CareType.GiveMeds))
                {
                    CareRoutine.Add(CareType.GiveMeds);
                }
                else
                {
                    Console.WriteLine("Already added.");
                }
            }
            else
            {
                Console.WriteLine("This care routine cannot be added");
            }
        }

        public void AddCareRoutine(int index) //add care routine to a pet from options by care type index
        {
            if (index == 1)
            {
                if (!CareRoutine.Contains(CareType.Bathe))
                {
                    CareRoutine.Add(CareType.Bathe);
                }
                else
                {
                    Console.WriteLine("Already added.");
                }
            }
            else if (index == 2)
            {
                if (!CareRoutine.Contains(CareType.Walk))
                {
                    CareRoutine.Add(CareType.Walk);
                }
                else
                {
                    Console.WriteLine("Already added.");
                }
            }
            else if (index == 3)
            {
                if (!CareRoutine.Contains(CareType.Comb))
                {
                    CareRoutine.Add(CareType.Comb);
                }
                else
                {
                    Console.WriteLine("Already added.");
                }
            }
            else if (index == 4)
            {
                if (!CareRoutine.Contains(CareType.Feed))
                {
                    CareRoutine.Add(CareType.Feed);
                }
                else
                {
                    Console.WriteLine("Already added.");
                }
            }
            else if (index == 5)
            {
                if (!CareRoutine.Contains(CareType.TakeToVet))
                {
                    CareRoutine.Add(CareType.TakeToVet);
                }
                else
                {
                    Console.WriteLine("Already added.");
                }
            }
            else if (index == 6)
            {
                if (!CareRoutine.Contains(CareType.Play))
                {
                    CareRoutine.Add(CareType.Play);
                }
                else
                {
                    Console.WriteLine("Already added.");
                }
            }
            else if (index == 7)
            {
                if (!CareRoutine.Contains(CareType.GiveMeds))
                {
                    CareRoutine.Add(CareType.GiveMeds);
                }
                else
                {
                    Console.WriteLine("Already added.");
                }
            }
            else
            {
                Console.WriteLine("There is no care routine with the given index");
            }
        }

        public void RemoveCareRoutine(int index) //remove a care routine with index from a pet
        {
            if (index == 1)
            {
                if (CareRoutine.Contains(CareType.Bathe))
                {
                    CareRoutine.Remove(CareType.Bathe);
                }
                else
                {
                    Console.WriteLine("Already added.");
                }
            }
            else if (index == 2)
            {
                if (CareRoutine.Contains(CareType.Walk))
                {
                    CareRoutine.Remove(CareType.Walk);
                }
                else
                {
                    Console.WriteLine("Already added.");
                }
            }
            else if (index == 3)
            {
                if (CareRoutine.Contains(CareType.Comb))
                {
                    CareRoutine.Remove(CareType.Comb);
                }
                else
                {
                    Console.WriteLine("Already added.");
                }
            }
            else if (index == 4)
            {
                if (CareRoutine.Contains(CareType.Feed))
                {
                    CareRoutine.Remove(CareType.Feed);
                }
                else
                {
                    Console.WriteLine("Already added.");
                }
            }
            else if (index == 5)
            {
                if (CareRoutine.Contains(CareType.TakeToVet))
                {
                    CareRoutine.Remove(CareType.TakeToVet);
                }
                else
                {
                    Console.WriteLine("Already added.");
                }
            }
            else if (index == 6)
            {
                if (CareRoutine.Contains(CareType.Play))
                {
                    CareRoutine.Remove(CareType.Play);
                }
                else
                {
                    Console.WriteLine("Already added.");
                }
            }
            else if (index == 7)
            {
                if (CareRoutine.Contains(CareType.GiveMeds))
                {
                    CareRoutine.Remove(CareType.GiveMeds);
                }
                else
                {
                    Console.WriteLine("Already added.");
                }
            }
            else
            {
                Console.WriteLine("There is no care routine with the given index");
            }
        }

        public void RemoveCareRoutine(String careType) //remove a care routine with name from a pet
        {
            careType = careType.ToUpper();
            if (careType == "BATHE")
            {
                if (CareRoutine.Contains(CareType.Bathe))
                {
                    CareRoutine.Remove(CareType.Bathe);
                }
                else
                {
                    Console.WriteLine("Not in the list.");
                }
            }
            else if (careType == "WALK")
            {
                if (CareRoutine.Contains(CareType.Walk))
                {
                    CareRoutine.Remove(CareType.Walk);
                }
                else
                {
                    Console.WriteLine("Not in the list.");
                }
            }
            else if (careType == "COMB")
            {
                if (CareRoutine.Contains(CareType.Comb))
                {
                    CareRoutine.Remove(CareType.Comb);
                }
                else
                {
                    Console.WriteLine("Not in the list.");
                }
            }
            else if (careType == "FEED")
            {
                if (CareRoutine.Contains(CareType.Feed))
                {
                    CareRoutine.Remove(CareType.Feed);
                }
                else
                {
                    Console.WriteLine("Not in the list.");
                }
            }
            else if (careType == "TAKE TO VET")
            {
                if (CareRoutine.Contains(CareType.TakeToVet))
                {
                    CareRoutine.Remove(CareType.TakeToVet);
                }
                else
                {
                    Console.WriteLine("Not in the list.");
                }
            }
            else if (careType == "PLAY")
            {
                if (CareRoutine.Contains(CareType.Play))
                {
                    CareRoutine.Remove(CareType.Play);
                }
                else
                {
                    Console.WriteLine("Not in the list.");
                }
            }
            else if (careType == "GIVE MEDS")
            {
                if (CareRoutine.Contains(CareType.GiveMeds))
                {
                    CareRoutine.Remove(CareType.GiveMeds);
                }
                else
                {
                    Console.WriteLine("Not in the list.");
                }
            }
            else
            {
                Console.WriteLine("This care routine cannot be added");
            }
        }


        public String ListCareRoutine() //returns the list of care routines of pet
        {
            string routine = "";
            int i = 1;
            foreach (CareType careType in CareRoutine)
            {
                routine += careType;
                routine += i != CareRoutine.Count ? ", " : "";
                i++;
            }

            return routine;
        }

        public override string ToString() //print out the details of a pet
        {
            String petInfo = Name + ": " + "a " + Age + " years old " + Species + "(" + Breed + ")";
            petInfo += "\nCare Routine: " + ListCareRoutine() + "\n";

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

    public enum CareType //to handle care types
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