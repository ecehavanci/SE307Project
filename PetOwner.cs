using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace SE307Project
{
    public class PetOwner : User
    {
        public PetOwner()
        {
            PetSitterContacts = new List<(PetSitter, bool)>();
            Pets = new List<Pet>();
        }

        public List<(PetSitter, bool)> PetSitterContacts; //bool represents if the pet sitter is hired

        public List<Pet> Pets;

        public PetOwner(string name, string surname, string email, string password) : base(name,
            surname, email, password)
        {
            PetSitterContacts = new List<(PetSitter, bool)>();
            Pets = new List<Pet>();
        }

        //For adding a new pet to the PetOwner's Pets
        public void AddPet(String name, int age, String breed, String species)
        {
            Pets.Add(new Pet(name, age, breed, species));
        }

        //If given pet name is in the list it returns the pet otherwise it returns null so a null reference exception
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

        //If given index is in range of the list size in the list it returns the pet otherwise it returns null so a 
        //null reference exception to be thrown and be handled
        public Pet FindPetInPets(int index)
        {
            if (index > Pets.Count - 1 || index < 0)
            {
                return null;
            }
            else
            {
                return Pets[index];
            }
        }

        //Deleting a pet with given name without exception
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

        //Deleting a pet with given index without exception
        public void DeletePet(int index)
        {
            if (index > Pets.Count || index < 0)
            {
                Console.WriteLine("Index must be between 1 and " + Pets.Count);
            }
            else
            {
                Pets.RemoveAt(index);
            }
        }

        //Editing a pet with given name
        public int EditPet(String PetName)
        {
            Pet pet = FindPetInPets(PetName);

            if (pet == null)
            {
                return 0;
            }

            Console.WriteLine("Enter new properties, enter -1 to skip");


            String petName;
            while (true)
            {
                Console.Write("Name (" + pet.Name + "): ");
                petName = Console.ReadLine();
                if (petName == "-1")
                {
                    break;
                }

                if (!petName.All(Char.IsLetter) || String.IsNullOrEmpty(petName))
                {
                    Console.WriteLine("Name is invalid");
                }
                else
                {
                    pet.Name = petName;
                    break;
                }
            }


            while (true)
            {
                Console.Write("Age (" + pet.Age + "): ");
                String age = Console.ReadLine();
                if (age != "-1")
                {
                    try
                    {
                        int ageNumeric = int.Parse(age);
                        if (ageNumeric < 0)
                        {
                            pet.Age = ageNumeric;
                        }

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
            Console.Write("Species (" + pet.GetSpecies() + "): ");
            String species = Console.ReadLine();

            if (species != "-1")
            {
                pet.SetSpecies(species);
            }

            String petBreed;
            while (true)
            {
                Console.Write("Breed (" + pet.Breed + "): ");
                petBreed = Console.ReadLine();
                if (petBreed == "-1")
                {
                    break;
                }

                if (petBreed.Any(Char.IsDigit) || petBreed.Any(Char.IsPunctuation))
                {
                    Console.WriteLine("Breed is invalid");
                }
                else
                {
                    pet.Breed = petBreed;
                    break;
                }
            }


            Console.Write("Current care routine: ");
            Console.WriteLine(pet.ListCareRoutine());

            Console.WriteLine();

            ListCareTypes();

            Console.WriteLine();

            String routine;

            Console.WriteLine("Please enter care routines to add, press -1 to stop");
            while (true)
            {
                routine = Console.ReadLine();
                if (routine == "-1")
                {
                    break;
                }

                try
                {
                    pet.AddCareRoutine(int.Parse(routine));
                }
                catch (FormatException e)
                {
                    pet.AddCareRoutine(routine);
                }
            }

            Console.WriteLine("Please enter care routines to remove from this pet, press -1 to stop");
            while (true)
            {
                routine = Console.ReadLine();
                if (routine == "-1")
                {
                    break;
                }

                try
                {
                    pet.RemoveCareRoutine(int.Parse(routine));
                }
                catch (FormatException e)
                {
                    pet.RemoveCareRoutine(routine);
                }
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

            String petName;
            while (true)
            {
                Console.Write("Name (" + pet.Name + "): ");
                petName = Console.ReadLine();
                if (petName == "-1")
                {
                    break;
                }

                if (!petName.All(Char.IsLetter) || String.IsNullOrEmpty(petName))
                {
                    Console.WriteLine("Name is invalid");
                }
                else
                {
                    pet.Name = petName;
                    break;
                }
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

            String petBreed;
            while (true)
            {
                Console.Write("Breed (" + pet.Breed + "): ");
                petBreed = Console.ReadLine();
                if (petBreed == "-1")
                {
                    break;
                }

                if (petBreed.Any(Char.IsDigit) || petBreed.Any(Char.IsPunctuation))
                {
                    Console.WriteLine("Breed is invalid");
                }
                else
                {
                    pet.Breed = petBreed;
                    break;
                }
            }


            Console.Write("Current care routine: ");
            Console.WriteLine(pet.ListCareRoutine());

            Console.WriteLine();

            ListCareTypes();

            Console.WriteLine();
            String routine;

            Console.WriteLine("Please enter care routines to add, press -1 to stop");
            while (true)
            {
                routine = Console.ReadLine();
                if (routine == "-1")
                {
                    break;
                }

                try
                {
                    pet.AddCareRoutine(int.Parse(routine));
                }
                catch (FormatException e)
                {
                    pet.AddCareRoutine(routine);
                }
            }

            if (pet.CareRoutine.Count == 0)
            {
                return 1;
            }

            Console.WriteLine("Please enter care routines to remove from this pet, press -1 to stop");
            while (true)
            {
                routine = Console.ReadLine();
                if (routine == "-1")
                {
                    break;
                }

                try
                {
                    pet.RemoveCareRoutine(int.Parse(routine));
                }
                catch (FormatException e)
                {
                    pet.RemoveCareRoutine(routine);
                }
            }

            return 1;
        }

        //Pet is created by getting input from user
        private void CreatePet()
        {
            Console.WriteLine("Please enter pet data");

            //Name is checked to only consist letter
            String petName;
            while (true)
            {
                Console.Write("Name: ");
                petName = Console.ReadLine();
                if (!petName.All(Char.IsLetter) || String.IsNullOrEmpty(petName))
                {
                    Console.WriteLine("Name is invalid");
                }
                else
                {
                    break;
                }
            }

            //Age is checked to only consist digits and be greater than or equal to 0
            int petAge = 0;

            while (true)
            {
                Console.Write("Age: ");
                try
                {
                    String petAgeInput = Console.ReadLine();
                    if (!petAgeInput.All(Char.IsDigit))
                    {
                        Console.WriteLine("Age is invalid");
                        continue;
                    }

                    petAge = int.Parse(petAgeInput);
                    if (petAge < 0)
                    {
                        Console.WriteLine("Age is invalid");
                        continue;
                    }

                    break;
                }
                catch (FormatException e)
                {
                    Console.WriteLine("Age must be entered in numeric format.");
                }
            }

            //Species is checked to be only given options if any other input is given it is set to undefined
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


            //Breed is checked not to consist any numbers or punctuation
            String petBreed;
            while (true)
            {
                Console.Write("Breed: ");
                petBreed = Console.ReadLine();
                if (petBreed.Any(Char.IsDigit) || petBreed.Any(Char.IsPunctuation))
                {
                    Console.WriteLine("Breed is invalid");
                }
                else
                {
                    break;
                }
            }

            AddPet(petName, petAge, petBreed, petSpecies);
            Console.WriteLine(petName + " is added to your pets.");
        }

        //Editing personal information like name, surname, email, password and location
        private bool EditPersonalInformation()
        {
            String location = String.IsNullOrEmpty(Location) ? "Unknown" : Location;
            Console.WriteLine(
                "Which part of your profile you want to edit? \n1) Name (" + Name + ")\n2) Surname (" + Surname +
                ")\n3) Email (" + Email + ")\n4) Password \n5) Location (" + location + ") \n6) Exit");

            String editWhere = Console.ReadLine();
            switch (editWhere)
            {
                case "1":
                    Console.Write("New name: ");
                    string newName = Console.ReadLine();
                    //New name is checked not to consist any number or punctuation
                    if (newName.Any(char.IsDigit) && newName.Any(char.IsPunctuation) && !String.IsNullOrEmpty(newName))
                    {
                        Name = newName;
                    }
                    else
                    {
                        Console.WriteLine("Entered name is invalid.");
                    }

                    break;
                case "2":
                    Console.Write("New surname: ");
                    string newSurname = Console.ReadLine();
                    //New surname is checked not to consist any number or punctuation
                    if (newSurname.All(char.IsLetter) && newSurname.Any(char.IsPunctuation) &&
                        !String.IsNullOrEmpty(newSurname))
                    {
                        Surname = newSurname;
                    }
                    else
                    {
                        Console.WriteLine("Entered surname is invalid.");
                    }

                    break;
                case "3":
                    string newEmail;
                    while (true)
                    {
                        Console.WriteLine("New Email: ");
                        newEmail = Console.ReadLine();

                        //New email is checked to be in the example@email.com format
                        if (Regex.IsMatch(newEmail,
                                @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z"))
                        {
                            Database db = Database.GetInstance();
                            User u = db.FindUser(newEmail);
                            if (u == null)
                            {
                                Email = newEmail;
                                break;
                            }

                            if (u.Email == Email)
                            {
                                Console.WriteLine(
                                    "Entered email is your current email. Do you wish to exit without changing? (Y/N)");
                                if (Console.ReadLine().ToUpper() == "Y")
                                {
                                    break;
                                }

                                continue;
                            }

                            Console.WriteLine("There is another user with same email. Please try again.");
                            continue;
                        }

                        Console.WriteLine("Email is not valid. Please try again.");
                    }

                    Console.WriteLine();
                    break;
                case "4":
                    //Password is changed safely without showing it on the screen
                    ChangePassword();
                    break;
                case "5":
                    Console.Write(Location == null || Location == "" ? "Location: " : "New Location: ");
                    string newLoc = Console.ReadLine();
                    Location = newLoc;
                    break;
                case "6":
                    return true;
            }

            return false;
        }

        //List possible species for pets
        private void ListPetSpecies()
        {
            Console.WriteLine("Possible species");
            Console.WriteLine("1) Dog");
            Console.WriteLine("2) Cat");
            Console.WriteLine("3) Bird");
            Console.WriteLine("4) Hamster");
            Console.WriteLine("5) Exotic Animal");
        }

        //List possible care types for pets
        private void ListCareTypes()
        {
            Console.WriteLine("Possible care types");
            Console.WriteLine("1) Bathe");
            Console.WriteLine("2) Comb");
            Console.WriteLine("3) Walk");
            Console.WriteLine("4) Feed");
            Console.WriteLine("5) Take to vet");
            Console.WriteLine("6) Play");
            Console.WriteLine("7) Give meds");
        }

        //Editing profile: can be editing own info or pet info
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
                            //Adding a new pet
                            CreatePet();
                            break;
                        case "2":
                            //Editing an existing pet
                            if (Pets.Count == 0)
                            {
                                Console.WriteLine("You do not have a pet yet.");
                                continue;
                            }

                            //Showing all pets owned to enable user choose one from them
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
                            //Checking if Pets is empty
                            if (Pets.Count == 0)
                            {
                                Console.WriteLine("You do not have a pet yet.");
                                continue;
                            }

                            //Showing all pets owned to enable user choose one from them
                            Console.WriteLine("Your Pets:");
                            ListPets();
                            Console.WriteLine("Please enter the name/index of the pet you want to edit.");
                            String petInput2 = Console.ReadLine();

                            try
                            {
                                int petIndex = int.Parse(petInput2);
                                DeletePet(petIndex - 1);
                            }
                            catch (FormatException e)
                            {
                                //Deleting a pet from Pets
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

        //Send a pet sitter a message for asking permission to mark this PetSitter as hired to be able this PetOwner to
        //can make comments and give stars to this PetSitter later
        public void SendHiringMessage(PetSitter petSitter)
        {
            for (int i = 0; i < PetSitterContacts.Count; i++)
            {
                if (PetSitterContacts[i].Item1.Email == petSitter.Email)
                {
                    petSitter.AddMessage(new HiringMessage(petSitter.Email, Email, 1));
                    Console.WriteLine("Sending hiring message...");
                    return;
                }
            }

            Console.WriteLine("You cannot hire a pet sitter that you did not send a request before or did not accept your request.");
        }

        //Marking a PetSitter as hired so this PetOwner can make comments and give stars to this PetSitter
        public void HirePetSitter(PetSitter petSitter)
        {
            for (int i = 0; i < PetSitterContacts.Count; i++)
            {
                if (PetSitterContacts[i].Item1.Email == petSitter.Email)
                {
                    PetSitterContacts[i] = (petSitter, true);
                    Console.WriteLine("Marked as hired");
                    return;
                }
            }

            Console.WriteLine("Request accepting problem, try again later.");
        }

        //Show all pets
        private void ListPets()
        {
            for (int i = 0; i < Pets.Count; i++)
            {
                Console.WriteLine(i + 1 + ") " + Pets[i].Name);
            }
        }

        //Adding a pet sitter to pet owners contacts so they can communicate
        public void AddToPetSitters(PetSitter petSitter)
        {
            PetSitterContacts.Add((petSitter, false));
        }

        //Making a comment to a pet sitter for other PetOwners to see and decide more easily on this PetSitter
        public void MakeCommentToPetSitter(PetSitter petSitter)
        {
            foreach (var psInfo in PetSitterContacts)
            {
                if (psInfo.Item1.Email == petSitter.Email)
                {
                    if (psInfo.Item2)
                    {
                        Console.WriteLine("Enter your comment here:");
                        String comment = Console.ReadLine();
                        int rate = RatePetSitter();
                        petSitter.AddComment(new Comment(Name, rate, comment));
                        return;
                    }
                    else
                    {
                        Console.WriteLine("You need to hire this pet sitter first in order to  make a comment.");
                        return;
                    }
                }
            }

            Console.WriteLine(
                "You do not have contact with this pet sitter, in order to have contact please send a request.");
        }

        //Giving a rate out of 5 to a PetSitter for other PetOwners to see and decide more easily on this PetSitter
        public int RatePetSitter()
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

        //Sending a message to a PetSitter so PetOwners and PetSitters can communicate and agree on the duties of the
        //PetSitter and when to start pet sitting
        public void SendMessageToPetSitter(PetSitter petSitter)
        {
            if (PetSitterContacts.Contains((petSitter, false)))
            {
                Console.WriteLine("Message:");
                String messageText = Console.ReadLine();
                Message message = new Message(Email, petSitter.Email, messageText);
                petSitter.AddMessage(message);
                AddMessage(message);
            }

            Console.WriteLine("Message sent!");
        }

        //Sending a request to PetSitter and asking PetSitter's permission to communicate so they can discuss details 
        //with messages
        public void SendRequestToPetSitter(PetSitter petSitter)
        {
            //PetOwner has  to have at least one Pet to send a request because request includes the info of which pet
            //to be taken care of
            if (Pets.Count == 0)
            {
                Console.WriteLine("You have to have at least one pet to send a request.");
                return;
            }

            Console.WriteLine("Please choose the animals you want to hire this pet sitter for");
            ListPets();

            List<Pet> pets = new List<Pet>();

            Console.WriteLine("Enter a pet name or index to add to the request, enter -1 when enough pets are added.");
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
                    pet = FindPetInPets(int.Parse(petInput) - 1);
                    if (pet != null)
                    {
                        if (pets.Contains(pet))
                        {
                            Console.WriteLine("Skipping...");
                        }
                        else
                        {
                            pets.Add(pet);
                        }
                    }
                    else
                    {
                        Console.WriteLine("There is no pet with given name/index.");
                    }
                }
                catch (FormatException e)
                {
                    pet = FindPetInPets(petInput);
                    if (pets.Contains(pet))
                    {
                        Console.WriteLine("Skipping...");
                    }
                    else
                    {
                        pets.Add(pet);
                    }
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
                Request request = new Request(Name + " " + Surname, Email, pets, Location);
                petSitter.AddRequest(request);
                Console.WriteLine("Request is sent");
            }
            else
            {
                Console.WriteLine("Request is canceled");
            }
        }

        //Listing the PetSitters this PetOwner has sent a request and got accepted before
        private void ListContacts()
        {
            Console.WriteLine("You are in contact with:");
            for (int i = 0; i < PetSitterContacts.Count; i++)
            {
                Console.WriteLine(i + 1 + ") " + PetSitterContacts[i].Item1.Name + " " +
                                  PetSitterContacts[i].Item1.Surname);
            }
        }

        //Showing profile
        public override void ShowProfile()
        {
            base.ShowProfile();
            Console.Write("Pets: " + ToString() + "\n");
            
        }

        //Reading messages 
        public override void ReadMessages()
        {
            bool isFirstHiringMessage = true;
            foreach (var message in MessageBox)
            {
                if (message.SenderMail == "system")
                {
                    if (isFirstHiringMessage)
                    {
                        Console.WriteLine("There are people who rejected your hiring requests.");
                        isFirstHiringMessage = false;
                    }

                    Console.WriteLine(message);
                }
            }

            PetSitter psToSend = null;
            bool willSendMessage = false;
            if (PetSitterContacts.Count == 0)
            {
                Console.WriteLine("You do not have any contact with a pet sitter yet. To establish contact you should" +
                                  " send a request to a pet sitter. You can list pet sitters through the main menu and" +
                                  " choose one to send request");
                return;
            }

            ListContacts();
            Console.WriteLine("Enter the index of the pet sitter you want to see messages of, enter -1 to exit");
            PetSitter messagesOfWhom = null;
            while (true)
            {
                try
                {
                    int messagesOfWhomIndex = int.Parse(Console.ReadLine());
                    if (messagesOfWhomIndex == -1)
                    {
                        return;
                    }

                    messagesOfWhom = FindPetSitter(messagesOfWhomIndex - 1);
                    break;
                }
                catch (FormatException e)
                {
                    Console.WriteLine("Please enter the index of whose messages you want to read or -1 to exit");
                }
                catch (IndexOutOfRangeException e)
                {
                    Console.WriteLine("Please enter a number between 1-" + PetSitterContacts.Count + " or -1");
                }
            }

            ShowMessagesFor(messagesOfWhom);
            Console.WriteLine("Do you want to send message to " + messagesOfWhom.Name + "? (Y/N)");
            if (Console.ReadLine().ToUpper() == "Y")
            {
                SendMessageToPetSitter(messagesOfWhom);
            }
        }

        private PetSitter FindPetSitter(int index)
        {
            try
            {
                return PetSitterContacts[index].Item1;
            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine("No pet sitter at the given index.");
            }

            return null;
        }

        public override String ToString()
        {
            String pets = "";
            for (int i = 0; i < Pets.Count; i++)
            {
                pets += Pets[i].Name + "("+ Pets[i].Breed +")";
                if (Pets.Count()-1!= i)
                {
                    pets += ", ";
                }
            }
            return pets;
            
        }
    }
}