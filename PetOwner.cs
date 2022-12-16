using System;
using System.Collections.Generic;
using System.Text;

namespace SE307Project
{
    public class PetOwner : User
    {
        private List<PetSitter> HiredPetSitters;
        private List<Pet> Pets;

        public PetOwner(string name, string surname, string email, string password, string Location) : base(name,
            surname, email, password, Location)
        {
            HiredPetSitters = new List<PetSitter>();
            Pets = new List<Pet>();
        }

        public void AddPet(String name, int age, String breed, AnimalType species)
        {
            Pets.Add(new Pet(name, age, breed, species));
        }

        //If given pet name is in the list it returns the pet othervise it returns null so a null reference exception
        //to be thrown and be handled
        public Pet FindPetInPets(String PetName)
        {
            foreach (var pet in Pets)
            {
                if (PetName == pet._Name)
                {
                    return pet;
                }
            }

            return null;
        }

        public void DeletePet(String PetName)
        {
            try
            {
                Pets.Remove(FindPetInPets(PetName));
            }
            catch (NullReferenceException n)
            {
                Console.WriteLine("There is no pet with the given name");
            }
        }

        public void DeletePet(int index)
        {
            Pets.RemoveAt(index);
        }

        public int EditPet(String PetName)
        {
            Pet pet;
            try
            {
                pet = FindPetInPets(PetName);
            }
            catch (NullReferenceException n)
            {
                Console.WriteLine("There is no pet with the given name");
                return -1;
            }

            Console.WriteLine("Enter new properties, enter -1 to skip");

            Console.Write("Name: ");
            String name = Console.ReadLine();

            Console.Write("Age: ");
            String age = Console.ReadLine();

            ListPetSpecies();
            Console.Write("Species: ");
            String species = Console.ReadLine();

            Console.Write("Breed: ");
            String breed = Console.ReadLine();

            if (name != "-1")
            {
                pet._Name = name;
            }
            
            if (age != "-1")
            {
                pet._Age = int.Parse(age);
            }
            
            if (species != "-1")
            {
                pet._Species = species;
            }
            
            if (breed != "-1")
            {
                pet._Breed = breed;
            }

            return 0;
        }

        public void ListPetSpecies()
        {
            Console.WriteLine("Possible species");
            Console.WriteLine("1) Dog");
            Console.WriteLine("2) Cat");
            Console.WriteLine("3) Bird");
            Console.WriteLine("4) Hamster");
            Console.WriteLine("5) Exotic Animal");
        }

        public void EditProfile()
        {
        }
        public void HirePetSitter(PetSitter petSitter)
        {
        }

        public void MakeCommentToPetSitter()
        {
        }

        public int RatePetSitter(PetSitter petSitter)
        {
            return 0;
        }

        public void SendMessageToPetSitter(PetSitter petSitter)
        {
        }

        public void SendRequestToPetSitter(PetSitter petSitter)
        {
        }

        public void ShowMessagesFor(String UserName)
        {
        }
    }
}