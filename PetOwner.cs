using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SE307Project
{
    public class PetOwner : User
    {
        private List<PetSitter> HiredPetSitters;
        private List<Pet> Pets;
        public PetOwner(string name, string surname, string email, string password) : base(name,
            surname, email, password)
        {
            HiredPetSitters = new List<PetSitter>();
            Pets = new List<Pet>();
        }

        public PetOwner()
        {
            
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

        public Pet FindPetInPets(int index)
        {
            try
            {
                return Pets[index];
            }
            catch (IndexOutOfRangeException i)
            {
                return null;
            }
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

            if (name != "-1")
            {
                pet._Name = name;
            }
            
            Console.Write("Age: ");
            String age = Console.ReadLine();

            if (age != "-1")
            {
                try
                {
                    pet._Age = int.Parse(age);
                }
                catch (FormatException e)
                {
                    Console.WriteLine("Age should be given in numeric format");
                }
            }
            
            ListPetSpecies();
            Console.Write("Species: ");
            String species = Console.ReadLine();

            if (species != "-1")
            {
                pet._Species = species;
            }
            
            Console.Write("Breed: ");
            String breed = Console.ReadLine();

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

        public override void EditProfile()
        {
            bool isExit = false;
            while (!isExit)
            {
                Console.WriteLine(
                    "Which part of your profile you want to edit? \n1) Name \n2) Surname \n3) Email \n4) Location \n5) Exit");
                int editWhere = Int32.Parse(Console.ReadLine());
                switch (editWhere)
                {
                    case 1:
                        Console.WriteLine("Write new name");
                        string newName = Console.ReadLine();
                        Name = newName;
                        break;
                    case 2:
                        Console.WriteLine("Write new surname");
                        string newSurname = Console.ReadLine();
                        this.Surname = newSurname;
                        break;
                    case 3:
                        Console.WriteLine("Write new email");
                        string newMail = Console.ReadLine();
                        Email = newMail;
                        break;
                    case 4:
                        Console.WriteLine("Write new location");
                        string newLoc = Console.ReadLine();
                        Location = newLoc;
                        break;
                    case 5:
                        isExit = true;
                        break;
                }
            }
            
        }

        public void HirePetSitter(PetSitter petSitter)
        {
            HiredPetSitters.Add(petSitter);
        }

        private void ListPets()
        {
            foreach (var pet in Pets)
            {
                Console.WriteLine(pet._Name);
            }
        }

        public void AddToPetSitters(PetSitter petSitter)
        {
            HiredPetSitters.Add(petSitter);
        }

        public void MakeCommentToPetSitter(PetSitter petSitter)
        {
            Console.WriteLine("Enter your comment here:");
            String comment = Console.ReadLine();
            int rate = RatePetSitter();
            petSitter.AddComment(new Comment(Name, rate, comment));
        }

        public int
            RatePetSitter( /*PetSitter petSitter*/) //Unfortunately we might have to delete pet sitter from parameters
        {
            while (true)
            {
                Console.WriteLine("Please enter rate out of five:");
                String rateString = Console.ReadLine();
                try
                {
                    int rate = int.Parse(rateString);
                    if (rate > 5 || rate < 1)
                    {
                        Console.WriteLine("Rate is inappropriate. Please try again.");
                    }
                    else
                    {
                        return rate;
                    }
                }
                catch (FormatException e)
                {
                    Console.WriteLine("Please enter the rate in numeric format.");
                }
            }
        }

        public void SendMessageToPetSitter(PetSitter petSitter)
        {
            if (HiredPetSitters.Contains(petSitter))
            {
                Console.WriteLine("Message:");
                String messageText = Console.ReadLine();
                Message message = new Message(Email, petSitter._Email, messageText);
                petSitter.AddMessage(message);
                AddMessage(message);
            }
        }

        public void SendRequestToPetSitter(PetSitter petSitter)
        {
            Console.WriteLine("Please choose the animals you want to hire this pet sitter for");
            ListPets();

            ArrayList pets = new ArrayList();

            while (true)
            {
                Pet pet;
                Console.WriteLine("Enter a pet name to add to the request, enter -1 when enough pets are added.");
                String petInput = Console.ReadLine();

                if (petInput == "-1")
                {
                    break;
                }

                try
                {
                    pet = FindPetInPets(int.Parse(petInput));
                    pets.Add(pet);
                }
                catch (FormatException)
                {
                    pet = FindPetInPets(petInput);
                    pets.Add(pet);
                }
                catch (NullReferenceException)
                {
                    Console.WriteLine("Given pet name/index is wrong. Please try again.");
                }
            }

            Request request = new Request(this, pets);
            petSitter.AddRequest(request);
        }

        public override void ShowMessagesFor(String email)
        {
            PetSitter petSitter = FindPetSitter(email);
            if (petSitter!=null)
            {
                Console.WriteLine("Messages: ");
                foreach (var message in MessageBox)
                {
                    Console.WriteLine(message.ToString());
                }
            }
            else
            {
                Console.WriteLine("No connection with email " + email);
            }
        }

        private PetSitter FindPetSitter(String email)
        {
            foreach (var petSitter in HiredPetSitters)
            {
                if (email == petSitter._Email)
                {
                    return petSitter;
                }
            }

            return null;
        }

        public override String ToString()
        {
            return ""; // TODO: To be filled
            //throw new NotImplementedException();
        }
    }
}