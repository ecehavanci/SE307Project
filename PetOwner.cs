﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SE307Project
{
    public class PetOwner : User
    {
        private List<PetSitter> HiredPetSitters;
        private List<Pet> Pets;

        public PetOwner(string name, string surname, string email, string password, string Location) : base(name, surname, email, password, Location)
        {
            HiredPetSitters = new List<PetSitter>();
            Pets = new List<Pet>();
        }

        public void AddPet(Pet pet) //in UML it was String,int,String,String
        {
            
        }

        public void DeletePet(String PetName)
        {

        }

        public int EditPet()
        {
            return 0;
        }

        public void EditProfile()
        {

        }

        private Pet FindPetInPets(String PetName)
        {
            return new Pet();
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
