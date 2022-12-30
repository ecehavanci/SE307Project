﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace SE307Project
{
    public class PetOwner : User
    {
        public PetOwner()
        {
            HiredPetSitters = new List<PetSitter>();
            Pets = new List<Pet>();
        }

        public List<PetSitter> HiredPetSitters;

        public List<Pet> Pets;

        public PetOwner(string name, string surname, string email, string password) : base(name,
            surname, email, password)
        {
            HiredPetSitters = new List<PetSitter>();
            Pets = new List<Pet>();
        }

        public void AddPet(String name, int age, String breed, String species)
        {
            Pets.Add(new Pet(name, age, breed, species));
        }

        //If given pet name is in the list it returns the pet othervise it returns null so a null reference exception
        //to be thrown and be handled
        public Pet FindPetInPets(String PetName)
        {
            foreach (var pet in Pets)
            {
                if (PetName == pet.Name)
                {
                    return pet;
                }
            }

            return null;
        }

        public Pet FindPetInPets(int index)
        {
            if (index > Pets.Count-1 || index < 0)
            {
                return null;
            }
            else
            {
                return Pets[index];
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
            if (index>Pets.Count || index<0)
            {
                Console.WriteLine("Index must be between 1 and " + Pets.Count);
            }
            else
            {
                Pets.RemoveAt(index);

            }
        }

        public int EditPet(String PetName)
        {
            Pet pet = FindPetInPets(PetName);

            if (pet == null)
            {
                return 0;
            }

            Console.WriteLine("Enter new properties, enter -1 to skip");

            Console.Write("Name(" + pet.Name + "): ");
            String name = Console.ReadLine();

            if (name != "-1")
            {
                pet.Name = name;
            }

            

            while (true)
            {
                Console.Write("Age(" + pet.Age + "): ");
                String age = Console.ReadLine();
                if (age != "-1")
                {
                    try
                    {
                        pet.Age = int.Parse(age);
                        break;
                    }
                    catch (FormatException e)
                    {
                        Console.WriteLine("Age must be entered in numeric format.");
                    }
                }
                else
                {
                    break;
                }
            }


            ListPetSpecies();
            Console.Write("Species(" + pet.GetSpecies() + "): ");
            String species = Console.ReadLine();

            if (species != "-1")
            {
                pet.SetSpecies(species);
            }

            Console.Write("Breed:(" + pet.Breed + "): ");
            String breed = Console.ReadLine();

            if (breed != "-1")
            {
                pet.Breed = breed;
            }

            return 1;
        }


        public int EditPet(int petIndex)
        {
            Pet pet = FindPetInPets(petIndex);
            if (pet == null)
            {
                return 0;
            }

            Console.WriteLine("Enter new properties, enter -1 to skip");

            Console.Write("Name(" + pet.Name + "): ");
            String name = Console.ReadLine();

            if (name != "-1")
            {
                pet.Name = name;
            }

            

            while (true)
            {
                Console.Write("Age(" + pet.Age + "): ");
                String age = Console.ReadLine();
                if (age != "-1")
                {
                    try
                    {
                        pet.Age = int.Parse(age);
                        break;
                    }
                    catch (FormatException e)
                    {
                        Console.WriteLine("Age must be entered in numeric format.");
                    }
                }
                else
                {
                    break;
                }
            }


            ListPetSpecies();
            Console.Write("Species(" + pet.GetSpecies() + "): ");
            String species = Console.ReadLine();

            if (species != "-1")
            {
                pet.SetSpecies(species);
            }

            Console.Write("Breed:(" + pet.Breed + "): ");
            String breed = Console.ReadLine();

            if (breed != "-1")
            {
                pet.Breed = breed;
            }

            return 1;

        }

        private void CreatePet()
        {
            Console.WriteLine("Please enter pet data");

            Console.Write("Name: ");
            String petName = Console.ReadLine();

            int petAge = 0;

            while (true)
            {
                Console.Write("Age: ");
                try
                {
                    petAge = int.Parse(Console.ReadLine());
                    break;
                }
                catch (FormatException e)
                {
                    Console.WriteLine("Age must be entered in numeric format.");
                }
            }

            ListPetSpecies();
            Console.Write("Species: ");
            String petSpecies = "";
            try
            {
                petSpecies = Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Console.Write("Breed: ");

            String petBreed = Console.ReadLine();

            Pets.Add(new Pet(petName, petAge, petBreed, petSpecies));
            Console.WriteLine(petName + " is added to your pets.");
        }

        private bool EditPersonalInformation()
        {
            String location = String.IsNullOrEmpty(Location) ? "Unknown" : Location;
            Console.WriteLine(
                "Which part of your profile you want to edit? \n1) Name(" + Name + ")\n2) Surname(" + Surname +
                ")\n3) Email(" + Email + ")\n4) Location(" + location + ") \n5) Exit");
            int editWhere = Int32.Parse(Console.ReadLine());
            switch (editWhere)
            {
                case 1:
                    Console.Write("New name: ");
                    string newName = Console.ReadLine();
                    Name = newName;
                    break;
                case 2:
                    Console.Write("New surname: ");
                    string newSurname = Console.ReadLine();
                    Surname = newSurname;
                    break;
                case 3:
                    Console.Write("New email:");
                    string newMail = Console.ReadLine();
                    Email = newMail;
                    break;
                case 4:
                    Console.Write(Location == null || Location == "" ? "Location: " : "New Location: ");
                    string newLoc = Console.ReadLine();
                    Location = newLoc;
                    break;
                case 5:
                    return true;
            }

            return false;
        }

        private void ListPetSpecies()
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
            bool isDone = false;
            Console.WriteLine("What do you want to edit? \n1) Personal Information\n2) Pets\n3) Exit");
            String editChoice = Console.ReadLine();

            if (editChoice == "1")
            {
                while (!isDone)
                {
                    isDone = EditPersonalInformation();
                }

                Console.WriteLine();
            }

            else if (editChoice == "2")
            {
                while (!isDone)
                {
                    Console.WriteLine("Please choose an action \n1) Add pet\n2) Edit pet \n3) Delete pet\n4) Exit");
                    String petChoice = Console.ReadLine();
                    switch (petChoice)
                    {
                        case "1":
                            CreatePet();
                            break;
                        case "2":
                            Console.WriteLine("Your Pets:");
                            ListPets();
                            Console.WriteLine("Please enter the name/index of the pet you want to edit");
                            String petInput = Console.ReadLine();

                            try
                            {
                                int petIndex = int.Parse(petInput);
                                int result = EditPet(petIndex - 1);

                                if (result == 0)
                                {
                                    Console.WriteLine("There is no pet with in the index " + petIndex);
                                }

                                if (result == -1)
                                {
                                    Console.WriteLine("Given index is not valid");
                                }
                            }
                            catch (FormatException e)
                            {
                                int result = EditPet(petInput);
                                if (result == 0)
                                {
                                    Console.WriteLine("There is no pet with name " + petInput);
                                }
                            }


                            break;
                        case "3":
                            Console.WriteLine("Your Pets:");
                            ListPets();
                            Console.WriteLine("Please enter the name/index of the pet you want to edit");
                            String petInput2 = Console.ReadLine();

                            try
                            {
                                int petIndex = int.Parse(petInput2);
                                DeletePet(petIndex-1);
                            }
                            catch (FormatException e)
                            {
                                DeletePet(petInput2);
                            }
                            break;
                        case "4":
                            isDone = true;
                            break;
                    }
                }
            }
        }

        public void HirePetSitter(PetSitter petSitter)
        {
            HiredPetSitters.Add(petSitter);
        }

        private void ListPets()
        {
            for (int i = 0; i < Pets.Count; i++)
            {
                Console.WriteLine(i + 1 + ") " + Pets[i].Name);
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
            RatePetSitter(
                /*PetSitter petSitter*/) //Unfortunately we might have to delete pet sitter from parameters
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
                Message message = new Message(Email, petSitter.Email, messageText);
                petSitter.AddMessage(message);
                AddMessage(message);
            }
        }

        public void SendRequestToPetSitter(PetSitter petSitter)
        {
            if (Pets.Count == 0)
            {
                Console.WriteLine("You have to have at least one pet to send a request.");
                return;
            }

            Console.WriteLine("Please choose the animals you want to hire this pet sitter for");
            ListPets();

            ArrayList pets = new ArrayList();

            Console.WriteLine("Enter a pet name to add to the request, enter -1 when enough pets are added.");
            while (true)
            {
                Pet pet;
                String petInput = Console.ReadLine();

                if (petInput == "-1")
                {
                    break;
                }

                try
                {
                    pet = FindPetInPets(int.Parse(petInput)-1);
                    if (pet != null)
                    {
                        pets.Add(pet);
                    }
                    else
                    {
                        Console.WriteLine("There is no pet with given name.");
                    }
                }
                catch (FormatException e)
                {
                    pet = FindPetInPets(petInput);
                    pets.Add(pet);
                }
                catch (NullReferenceException e)
                {
                    Console.WriteLine("Given pet name/index is wrong. Please try again.");
                }
            }

            if (pets.Count == 0)
            {
                Console.WriteLine("You have to choose at least one pet to send a request.");
                return;
            }

            Console.Write("Sending request for ");
            foreach (Pet pet in pets)
            {
                Console.Write(pet.Name + " ");
            }

            Console.WriteLine("\nDo you confirm? (Y/N)");
            String yesNoInput = Console.ReadLine();
            if (yesNoInput.ToUpper() == "Y")
            {
                Request request = new Request(this, pets);
                petSitter.AddRequest(request);
                Console.WriteLine("Request is sent");
            }
            else
            {
                Console.WriteLine("Request is canceled");
            }
            
        }

        public override void ShowProfile()
        {
            base.ShowProfile();
            Console.WriteLine("Pets:");
            foreach (var pet in Pets)
            {
                Console.WriteLine(pet.Name + "(" + pet.GetSpecies() + ")");
            }
        }

        public override void ShowMessagesFor(String email)
        {
            PetSitter petSitter = FindPetSitter(email);
            if (petSitter != null)
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
                if (email == petSitter.Email)
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