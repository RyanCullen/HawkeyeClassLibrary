using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HawkeyehvkBLL;
using HawkeyehvkDB;
using System.Collections.Generic;
using System.Data;

namespace HawkeyeUnitTests
{
    [TestClass]
    public class HawkeyeTests {


        //check that they follow last name order(currently hardcoded in stub to be in order)
        [TestMethod]
        public void listowners1() {
            Assert.AreEqual(19, Owner.listTheOwners().Count);

        }
        //check that they are last name then first name(relative to your listowners method)
        [TestMethod]
        public void listowners2() {
            Assert.AreEqual("Anita", Owner.listTheOwners()[0].firstName);
            Assert.AreEqual("Alibi", Owner.listTheOwners()[0].lastName);
        }
        //returns type list
        [TestMethod]
        public void listowners3() {
            Assert.AreEqual("Chester", Owner.getOwner(19).firstName, "Not  Returning First Name Chester");
            Assert.AreEqual("Drawers", Owner.getOwner(19).lastName, "Not  Returning Last Name Drawers");
        }



        //Must be sorted by pet number
        [TestMethod]
        public void listPets1() {

            Assert.AreEqual(3, Pet.listPets(2)[0].petNumber);
            Assert.AreEqual(6, Pet.listPets(2)[1].petNumber);
        }

        //Test with owner with 0 pets(Owner 14)
        [TestMethod]
        public void listPets2() {

            Assert.AreEqual(0, Pet.listPets(14).Count);
        }

        //Test with owner with 1 pet(owner 18 pet 33)
        [TestMethod]
        public void listPets3() {

            Assert.AreEqual(1, Pet.listPets(18).Count);
            Assert.AreEqual(33, Pet.listPets(18)[0].petNumber);
        }

        //Test with owner with multiple pets(1+) (Owner owner 7 pets: 10,11,12)
        [TestMethod]
        public void listPets4() {

            Assert.AreEqual(10, Pet.listPets(7)[0].petNumber);
            Assert.AreEqual(11, Pet.listPets(7)[1].petNumber);
            Assert.AreEqual(12, Pet.listPets(7)[2].petNumber);
        }

        //Invalid owner number
        [TestMethod]
        public void listPets5() {

            Assert.AreEqual(0, Pet.listPets(000).Count);
        }

        //Owner with multiple(Owner 4)
        [TestMethod]
        public void listReservations1() {
            Assert.AreEqual(620, Reservation.listReservations(4)[0].reservationNumber);
            Assert.AreEqual(631, Reservation.listReservations(4)[1].reservationNumber);
        }

        //Owner with 0 (Owner 5)
        [TestMethod]
        public void listReservations2() {
            Assert.AreEqual(0, Reservation.listReservations(5).Count);
        }

        //Invalid owner number returns null
        [TestMethod]
        public void listReservations3() {
            Assert.AreEqual(0, Reservation.listReservations(0000).Count);
        }

        //return all active reservation - Expected 3 hardcoded data
        [TestMethod]
        public void listActiveReservations1() {
            Assert.AreNotEqual(0, Reservation.listActiveReservations().Count);
        }


        [TestMethod]
        public void listActiveReservations2() {
            //Owner 4 - Expected : No Active Reservation 
            Assert.AreNotEqual(new System.Collections.Generic.List<Reservation>(), Reservation.listActiveReservations(4));
        }


        // need to provide an update query just to test
        [TestMethod]
        public void listActiveReservations3() {

            //owner 1 - expected : 1 active reservation 
            Assert.AreNotEqual(0, Reservation.listActiveReservations(1).Count);
        }

        [TestMethod]
        public void listActiveReservations4() {
            //Owner 2 - Expected : More than 1 active reservation 
            Assert.AreEqual(3, Reservation.listActiveReservations(2).Count);
            Assert.AreNotEqual(0, Reservation.listActiveReservations(2).Count);

        }

        [TestMethod]
        public void listActiveReservations5() {
            //Invalid Owner number expected : null 
            Assert.AreEqual(0, Reservation.listActiveReservations(000000).Count);
        }



        //Full vaccinations 
        [TestMethod]
        public void listVaccination1() {
            Assert.AreEqual(6, PetVaccination.listVaccinations(1).Count);
        }

        //Not Full Vaccination 
        [TestMethod]
        public void listVaccination2() {
            Assert.AreEqual(4, PetVaccination.listVaccinations(7).Count);
        }

        //Invalid Pet Number
        //Require Probe
        [TestMethod]
        public void listVaccination3() {
            Assert.AreEqual(0, PetVaccination.listVaccinations(0000).Count);
        }

        //No Vaccination
        [TestMethod]
        public void listVaccination4() {
            Assert.AreEqual(0, PetVaccination.listVaccinations(10).Count);
        }

        //Invalid Pet Number 
        //Needs Probe***
        [TestMethod]
        public void checkVaccination1() {
            Assert.AreEqual(0, PetVaccination.checkVaccinations(100, 0000).Count);
        }

        //Invalid Reservation Number 
        //Needs Probe ***
        [TestMethod]
        public void checkVaccination2() {
            Assert.AreEqual(0, PetVaccination.checkVaccinations(00000, 1).Count);
        }


        //Valid Vaccination - return 0 
        [TestMethod]
        public void checkVaccination3() {
            Assert.AreEqual(0, PetVaccination.checkVaccinations(3, 108).Count);
        }

        //Not Valid Vaccination - return 6 
        [TestMethod]
        public void checkVaccination4() {
            PetVaccination control = new PetVaccination();
            Assert.AreEqual(6, PetVaccination.checkVaccinations(1, 100).Count);
        }


        //Expired Vaccination 
        [TestMethod]
        public void checkVaccination5() {
            Assert.AreNotEqual(0, PetVaccination.checkVaccinations(6, 115).Count);
        }

        //Pet with non expired and unchecked vaccines
        [TestMethod]
        public void checkVaccination6() {
            Assert.AreEqual(6, PetVaccination.checkVaccinations(9, 708).Count);
        }


        //Pet with missing vaccinations
        [TestMethod]
        public void checkVaccination7() {
            Assert.AreEqual(6, PetVaccination.checkVaccinations(7, 620).Count);
        }


        //on entered date are returned(2017, 6, 20)
        [TestMethod]
        public void upcomingReservation1() {
            //return 6 you can aslo test with equal 6 
            Assert.AreNotEqual(0, Reservation.listUpcomingReservations(new DateTime(2017, 6, 20)).Count);
        }



        //No upcoming reservation past - (1999,06,15)
        [TestMethod]
        public void upcomingReservation2() {
            Assert.AreNotEqual(0, Reservation.listUpcomingReservations(new DateTime(2017, 6, 15)).Count);
        }


        //curentlly return 6 - you can also test with equal to  6 (2017-04-25)
        [TestMethod]
        public void upcomingReservation3() {
            Assert.AreNotEqual(0, Reservation.listUpcomingReservations(new DateTime(2017, 4, 25)).Count);
        }

        //on the same date 
        [TestMethod]
        public void upcomingReservation4() {
            //amir is here
            Assert.AreEqual(5, Reservation.listUpcomingReservations(new DateTime(2017, 8, 20)).Count);
        }


        //No upcoming reservation in future
        [TestMethod]
        public void upcomingReservation5() {
            Assert.AreEqual(0, Reservation.listUpcomingReservations(new DateTime(2050, 8, 28)).Count);
        }


        [TestClass]
        public class reservationModificationTests {


            [TestMethod]
            public void addReservationTest() {//addReservation(int petNumber, DateTime startDate, DateTime endDate)

                // invalid pet number test
                // Input Parameters: 
                //Pet number = 40
                //
                //Expected: -10 (invalid pet number)
                Assert.AreEqual(-10, Reservation.addReservation(40, DateTime.Now.AddDays(7), DateTime.Now.AddDays(10)), "invalid pet number test");


                // Start Date after end date
                // Input Parameters: Start: 
                // 14-MAY-17 - End
                // 13-MAY-17 - Start
                //Expected: -12 (start date after end date)
                Assert.AreEqual(-12, Reservation.addReservation(1, new DateTime(2017, 5, 14), new DateTime(2017, 5, 13)), " Start Date after end date test");


                // happy case
                // Input Parameters: 
                // pet number 35
                // Start: 4-MAR-17
                // end: 6-MAR-17
                // Expected: -1 (success)
                Assert.AreEqual(-1, Reservation.addReservation(35, new DateTime(2017, 4, 25), new DateTime(2017, 4, 26)), "happy case");


                // Start Date In the Past
                // Input Parameters: Start: 
                // 9-MAR-17 - End
                // 13-MAY-17 - Start
                //Expected: -11
                Assert.AreEqual(-11, Reservation.addReservation(1, new DateTime(2017, 3, 9), new DateTime(2017, 5, 13)), " Start Date after end date test");


                // Start date == end date
                // Input Parameters: 
                // 12-MAY-18 - End
                // 13-MAY-18 - Start
                //Expected: 0 (Success)
                //No Available runs for dog size, this will be tested when we have further understanding
                Assert.AreEqual(-14, Reservation.addReservation(1, new DateTime(2018, 5, 12), new DateTime(2018, 5, 13)), "Runs available when they're full");


                // Start date == end date
                // Input Parameters: 
                // 04-JAN-17 - End
                // 04-JAN-17 - Start
                //Expected: 0 (Success)
                Assert.AreEqual(-1, Reservation.addReservation(34, new DateTime(2017, 5, 24), new DateTime(2017, 5, 26)), "Start date == end date test");


                // Pet has reservation during those days
                // input parameters
                // pet number: 7 
                // (existing reservation:631) 
                // dates: 01-JAN-16 TO 04-JAN-16   
                //Expected: -13 (pet has reservation at that time already)
                Assert.AreEqual(-13, Reservation.addReservation(7, new DateTime(2017, 5, 12), new DateTime(2017, 5, 14)), "Pet has reservation during those days");



            }

            [TestMethod]
            public void addToReservationTest() {//addToReservation(int reservationNumber, int petNumber)

                // invalid pet number
                // Input Parameters: 
                //Pet number: 40
                //Expected: 1 (invalid pet number)
                Assert.AreEqual(-1, Reservation.addToReservation(601, 0000), "Invalid Pet number test");

                // Pet has reservation during those days
                // Input Parameters: 
                //pet number: 7 
                //reservation:631 
                //  Expected: 2 (Error pet has reservation at that time already)
                Reservation.addReservation(7,new DateTime(2016,1,1),new DateTime(2016,1,4));
                Assert.AreEqual(-3, Reservation.addToReservation(631, 7), "Pet has reservation during those days");


                // invalid reservation number
                // Input Parameters: 
                // reservation number: 900
                //Expected: -2 (invalid res number)
                Assert.AreEqual(-2, Reservation.addToReservation(0000, 7), "invalid reservation number test");

                //pets from different owners
                // Input Parameters: 
                // reservation number: 108
                // pet number:  1
                // 
                //Expected: -4 (dogs not from same owner)
                 Assert.AreEqual(-4, Reservation.addToReservation(108, 1), "pets from different owners");

            }



            [TestMethod]
            public void testChangeReservation() {

                Reservation hvk = new Reservation();

                //Intput parameters:
                //reservation Number: 999
                //Start date: 01-01-16
                //end Date: 15-01-16
                //Expected: 1
                Assert.AreEqual(1, Reservation.changeReservation(999, new DateTime(2016, 01, 16), new DateTime(2016, 01, 18)), "Invalid Reservation Number not returning 1");

                //Input parameters:
                //reservation number: 3
                //Start date:16 - JAN - 16
                //end date: 01-JAN-16
                //Expected: 2
                Assert.AreEqual(2, Reservation.changeReservation(800, new DateTime(2016, 01, 16), new DateTime(2016, 01, 01)), "Start Date After End Date Not Returning 2");

                //Input parameters:
                //reservation number: 6
                //Start date:01-JAN-16
                //end date: 15-JAN-16 
                //Expected: 3
                Assert.AreEqual(3, Reservation.changeReservation(605, new DateTime(2017, 03, 05), new DateTime(2017, 03, 09)), "No available runs returning Not Returning 3");

                //Input parameters:
                //reservation number: 605
                //Start date:5/3/2017
                //end date: 9/3/2017
                //Expected: 0
                //Happy Case AssertFalse as its the default for the empty method currently
                Assert.AreEqual(0, Reservation.changeReservation(800, new DateTime(2017, 03, 05), new DateTime(2017, 03, 09)), "Invalid Reservation Change");

            }

            [TestMethod]
            public void testCheckVaccinations() {

                //Input Parameters:
                //Pet Number: 999
                //date: 16/01/2016
                //Expected: 1
                //Invalid Pet Number
                Assert.AreEqual(-10, PetVaccination.checkVaccinations(999, new DateTime(16, 01, 16)), "Invalid Pet Number Not returning 1");

                //Input Parameters:
                //Pet Number: 14
                //date: 17/05/05
                //Expected: 2
                //Happy Path With 1 missing Vaccination
                Assert.AreEqual(-1, PetVaccination.checkVaccinations(14, new DateTime(17, 05, 05)), "Missing 1 Vaccination Not returning -1");

                //Input Parameters
                //Pet Number: 34
                //date: 17/05/05
                //Expected: -1
                //Happy Path Pet with no Vaccinations
                Assert.AreEqual(-1, PetVaccination.checkVaccinations(34, new DateTime(17, 05, 05)), "No Vaccination Not returning full list of vaccinations");

                //Input Parameters:
                //Pet Number: 3
                //date: 17-02-20
                //Expected: 0
                //Happy Path Valid Vaccinations
                Assert.AreEqual(0, PetVaccination.checkVaccinations(3, new DateTime(17, 02, 20)), "Not Returning Valid Vaccinations");
            }




            //Changes To The Methods Called to return the ints

            [TestMethod]
            public void RunAvailability() {

                //Run is not available (Return 0)
                DateTime startDate = new DateTime(2018, 5, 12);
                DateTime endDate = new DateTime(2018, 5, 13);
                Assert.AreEqual(0, Run.checkRunAvailability(startDate, endDate, 'R'), "There should be no runs availible for this time.");

                // Test with regular size (Returns number of runs)
                startDate = new DateTime(2016, 09, 12);
                endDate = new DateTime(2016, 9, 30);
                Assert.IsTrue(0 < Run.checkRunAvailability(startDate, endDate, 'R'), "During this time there should be multiple runs availible");
                // Test with large size (Returns number of runs)
                startDate = new DateTime(2016, 09, 12);
                endDate = new DateTime(2016, 9, 30);
                Assert.IsTrue(0 < Run.checkRunAvailability(startDate, endDate, 'L'), "During this time there should be multiple runs availible");
                //End date before start date (Return -1)
                startDate = new DateTime(2015, 09, 12);
                endDate = new DateTime(2014, 1, 31);
                Assert.AreEqual(-1, Run.checkRunAvailability(startDate, endDate, 'R'), "A request when start is after end date should return -1.");

                //test case
                // more regular pets than there are regular runs causing for regular sized pets to be in large runs.
                // the remaining runs are occupied by large dogs. Check run availibility for large dog should return 0 runs.
                // this test is important because regular and large runs are seperated.
                startDate = new DateTime(2018, 8, 16);
                endDate = new DateTime(2018, 8, 20);
                Assert.AreEqual(0, Run.checkRunAvailability(startDate, endDate, 'L'), "There should be no runs (Large or normal) availible for this time.");

                //test case 
                // test that our method works logically.
                // since our method takes the most busy day in the range of dates and checks that everything works there 
                // we want to test if the bussiest day has space for a large dog but the seccond bussiest day doesnt.
                // to force this the bussiest day has 7 reservations with some regular some large with large runs availible
                // the seccond most busy has 6 reservations but all large
                startDate = new DateTime(2018, 6, 17);
                endDate = new DateTime(2018, 6, 22);
                Assert.AreEqual(0, Run.checkRunAvailability(startDate, endDate, 'L'), "There should be no runs (Large) availible for this time.");


            }





            /* addToReservation Test Cases  */
            // reservation# 603 , owner# 17 , pet in reservation 31 , 32 
            //Input : pet# 30   Expected : 1 row inserted  

            [TestMethod]
            public void testAddOwner() {
                //This Test after being run will make listOwners fail as the list is increasing
                //Input
                //First Name: Brian || Last Name: Griffin || Street: 31 Spooner Street || City: Quahog || Province: QC || Postal: K9Y1Y2 || Phone: 1234567890 || Email: a@b.ca || Emergency Stuff : ""
                Owner.addOwner("Brian", "Griffin", "31 Spooner Street", "Quahog", "QC", "K9Y1Y2", "1234567890", "a@b.ca", "", "", "");

                //Getting the Owner Changes On sequence Value. On Fresh DB should return 250(start of seq.NEXTVAL)
                Assert.AreEqual("Brian", Owner.getOwner(250).firstName, "Not Returning first Name Brian");
                Assert.AreEqual("Griffin", Owner.getOwner(250).lastName, "Not Returning Last Name Griffin");


            }
            [TestMethod]
            public void testUpdateOwner() {
                //This Method Directly Relies on the testAddOwner Insert <<Please Leave this test right aftert testAddOwner>>

                Owner.updateOwner(250, "Alex", "Stewart", "My Street", "Quahog", "QC", "K9Y1Y2", "1234567890", "a@b.ca", "", "", "");
                Owner testOwner = Owner.getOwner(250);
                Assert.AreEqual("Alex", testOwner.firstName, "Not Returning first Name Alex");
                Assert.AreEqual("Stewart", testOwner.lastName, "Not Returning Last Name Stewart");
                Assert.AreEqual("My Street", testOwner.address.street, "Not Returning My Street");

            }


            [TestMethod]
            public void testAddReservationDiscount() {
                //Testing with the reservation number of 800

                //Success
                Assert.AreEqual(0, Discount.addReservationDiscount(2, 800), "Add Did not Succeed");

                //Fail Invalid Reservation Number
                Assert.AreEqual(-19, Discount.addReservationDiscount(2, 999), "Invalid Reservation Number Succeeded");

                //Test The Insert
                Assert.AreEqual(2, Discount.listReservationDiscounts(800)[0].discountNumber, "Did not return reservation discount of discNum 2");
            }




            [TestMethod]
            public void testDeleteReservationDiscount() {
                //Testing The Insert From testAddReservation

                //Fail Invalid Reservation Number
                Assert.AreEqual(-19, Discount.deleteReservationDiscount(2, 999), "Invalid Reservation Number Succeeded");

                //Success
                Assert.AreEqual(0, Discount.deleteReservationDiscount(2, 800), "Delete Did not Succeed");

                //Test The Deletion
                Assert.AreEqual(0, Discount.listReservationDiscounts(800).Count, "Returned an unexpected List of Reservaion Discount for ResNum 800");
            }
            [TestMethod]
            public void testDeleteDogFromReservation() {
                Reservation hvk = new Reservation();
                // Test Method: Solo pet in reservation
                // Input Parameters: reservationNumber - 108
                //                   petNumber - 3
                // Expected Result: 0
                Assert.AreEqual(0, Reservation.deleteDogFromReservation(108, 3), "Solo dog in reservation didn't return 0");
                //This should also delete the entire reservation since there was only one pet reservation
                Reservation.listReservations().ForEach(delegate (Reservation res) {
                    if (res.reservationNumber == 108) {
                        Assert.Fail("The reservation 108 was not deleted.");
                    }
                });
                // not test to ensure: HVK_RESERVATION_DISCOUNT, hvk_pet_reservation_service entries were deleted because 
                // they would have stopped the deletion of the pet reservation to begin with.

                //Check to make sure that the pet reservation is gone
                PetReservation.listPetRes(108).ForEach(delegate (PetReservation pr) {
                    if (pr.pet.petNumber == 3) {
                        Assert.Fail("Any pet reservation with Reservation number 108 and pet number 3 should not be there at this point.");
                    }
                });

                // Test Method: Sharing pet in reservation
                // Input Parameters: reservationNumber - 140
                //                   petNumber - 26
                // Expected Result: 0 (Pet that was being shared with must be set to solo)
                Assert.AreEqual(0, Reservation.deleteDogFromReservation(140, 26), "Sharing dog in reservation didn't return 0");

                // Test Method: Pet not part of the reservation
                // Input Parameters: reservationNumber - 140
                //                   petNumber - 1
                // Expected Result: 3
                Assert.AreEqual(3, Reservation.deleteDogFromReservation(140, 1), "Pet not in reservation didn't return 3");
                // Test Method: Invalid reservation number
                // Input Parameters: reservationNumber - 0
                //                   petNumber - 1
                // Expected Result: 1
                Assert.AreEqual(1, Reservation.deleteDogFromReservation(0, 1), "Invalid reservation number didn't return 1");
                // Test Method: Invalid pet number
                // Input Parameters: reservationNumber - 140
                //                   petNumber - 0
                // Expected Result: 2
                Assert.AreEqual(2, Reservation.deleteDogFromReservation(140, 0), "Invalid pet number didn't return 2");

                // This reservation was created in the opening script to be going on today. It should not work since an ongoing reservation cannot be modified
                Assert.AreEqual(4, Reservation.deleteDogFromReservation(500, 3), "cancel reservation that is ongoing cant be cancelled.");

                // Removing dog from reservation causing for them to lose discount
                // reservation 636 has 3 pet reservations. removing one pet should remove the entry in pet Reservation
                Assert.AreEqual(0, Reservation.deleteDogFromReservation(636, 6), "Removing a dog from a reservation with 3 pets should be successful.");


                // reservation goes from 3 to 2 dogs and therefor the discount should be deleted.
                if (Discount.listReservationDiscounts(636).Count > 0) {
                    Assert.Fail("After the deletion the reservation's discount should have been removved since it now has 2 pets.");
                }
            }
            [TestMethod]
            public void testCancelReservation() {

                //Reservation with one pet - Reservation 615
                //check pet_res's are gone
                Assert.AreEqual(0, Reservation.cancelReservation(615), "cancel reservation 615 not succesfull.");
                if (0 < PetReservation.listPetRes(615).Count) {
                    Assert.Fail("Deleting the reservation should also delete all pet reservations.");
                }

                // Reservation with multple pets
                // Reservation number 100
                Assert.AreEqual(0, Reservation.cancelReservation(100), "cancel reservation 100 not succesfull.");
                if (0 < PetReservation.listPetRes(100).Count) {
                    Assert.Fail("Deleting the reservation should also delete all pet reservations.");
                }

                //invalid reservation Number
                //reservation number 5
                Assert.AreEqual(1, Reservation.cancelReservation(5), "cancel reservation with invalid reservation number was not successfull.");

                // This reservation was created in the opening script to be going on today. It should not work
                Assert.AreEqual(4, Reservation.cancelReservation(500), "cancel reservation that is ongoing cant be cancelled.");


            }

            //Scenario Tests
            [TestMethod]
            public void testing1PetAddReservation()
            {
                //Pet Number 33
                //Owner number 19
                //Start Date 2017-4-12
                //End Date 2017-4-13
                Reservation.addReservation(33, new DateTime(2017, 4, 12), new DateTime(2017, 4, 13));

                Assert.AreEqual(new DateTime(2017, 4, 12), Reservation.listReservations(19)[1].startDate, "Not returning date 2017-4-12");
                Assert.AreEqual(new DateTime(2017, 4, 13), Reservation.listReservations(19)[1].endDate, "Not returning date 2017-4-13");
                Assert.AreEqual(33, PetReservation.listPetRes(2006)[0].pet.petNumber, "Not Returning the pet number 33");

                
            }

            /* addToReservation Test Cases  */
            // reservation# 603 , owner# 17 , pet in reservation 31 , 32 
            //Input : pet# 30   Expected : 1 row inserted  

            [TestMethod]
            public void testDBMethods() {
                /*
                 * Here i will do one test on each of the DB methods to ensure they have returned 
                */
                //----------------OwnerDB----------------
                OwnerDB ownDB = new HawkeyehvkDB.OwnerDB();
                //listOwnersDB
                DataSet ds = ownDB.listOwnersDB();
                Assert.AreEqual(20, ds.Tables[0].Rows.Count, "Count of 20 was expected from this function");

                //listOwnersDB(ownerNum) Owner 1 has phone: 8195551111
                Assert.AreEqual("8195551111", ownDB.listOwnersDB(1).Tables[0].Rows[0]["OWNER_PHONE"].ToString());

                //----------------PetDB----------------
                PetDB pet = new PetDB();
                //listPetsDB(ownerNum) owner 7 has 3 pets, 10,11,12
                Assert.AreEqual(3, pet.listPetsDB(7).Tables[0].Rows.Count, "Owner 7 has 3 pets.");
                //getOnePetDB(petNum) pet 13's name is Logan
                Assert.AreEqual("Logan", pet.getOnePetDB(13).Tables[0].Rows[0]["PET_NAME"], "Pet 13's name is logan");
                //checkPetsInReservation(resNum) res 713 has pets 26,27,28
                Assert.AreEqual(3, pet.checkPetsInReservation(713), "Reservation 713 has 3 pets");
                //----------------petReservation----------------
                PetReservationDB pres = new PetReservationDB();
                //listPetResDB(reservationNumber) res 713 has 3 pet reservations && res 700 has one (16)
                Assert.AreEqual(3, pres.listPetResDB(713).Tables[0].Rows.Count, "Reservation 713 has 3 pet reservations");
                ds = pres.listPetResDB(700);//res 700 has one (pet number 16)
                Assert.AreEqual(1, ds.Tables[0].Rows.Count, "reservation 700 has 1 pet");
                Assert.AreEqual("16", ds.Tables[0].Rows[0]["PET_PET_NUMBER"].ToString(), "Reservation 700 has one pet. Pet 16");
                
                //----------------runDB----------------
                RunDB run = new RunDB();
                //totalLargeRunsDB()
                Assert.AreEqual(6, run.totalLargeRunsDB(), "There should be 6 Large runs");
                //totalRegularRunsDB()
                Assert.AreEqual(6, run.totalRegularRunsDB(), "There should be 6 Regular runs");
                //getNumAvailableRuns(startDate, endDate)
                Assert.AreEqual(12, run.getNumAvailableRunsDB(DateTime.Now.AddYears(10), DateTime.Now.AddYears(10).AddDays(1)), "At this date there should be 0 reservations");
                Assert.IsTrue(0 >= run.getNumAvailableRunsDB(new DateTime(2018, 05, 12), new DateTime(2018, 05, 13)), "At this date there should be no availible runs");
                //getNumAvailableLargeRuns(startDate, endDate)
                Assert.AreEqual(6, run.getNumAvailableLargeRunsDB(DateTime.Now.AddYears(10), DateTime.Now.AddYears(10).AddDays(1)), "At this date there should be 0 reservations");
                Assert.IsTrue(0 >= run.getNumAvailableLargeRunsDB(new DateTime(2018, 05, 12), new DateTime(2018, 05, 13)), "At this date there should be no availible large runs");
                //----------------searchDB----------------
                SearchDB search = new SearchDB();
                //searchConflictingReservationsDB(petNumber,start,end)
                Assert.IsTrue(0 != search.searchConflictingReservations(1,DateTime.Now.AddMonths(-20), DateTime.Now),"Pet one has a reservation between 20 months ago and now.");
                Assert.IsTrue(0 == search.searchConflictingReservations(1, DateTime.Now.AddMonths(40), DateTime.Now.AddMonths(40)), "Pet one has no reservations 40 months from now");
                //getPetSize(petNum)
                Assert.AreEqual('S',search.getPetSize(1),"Pet one is size small");
                //searchPetDB(petNumber)
                Assert.AreEqual(1,search.searchPetDB(1),"pet Number 1 exists");
                Assert.AreNotEqual(1, search.searchPetDB(666), "pet Number 666 doesnt exist");
                //searchOwnerDB(ownerNum)
                Assert.AreEqual(1, search.searchOwnerDB(1), "owner Number 1 exists");
                Assert.AreNotEqual(1, search.searchOwnerDB(33), "owner Number 33 doesnt exist");
                //searchReservationsDB(resNum)
                Assert.AreEqual(1, search.searchReservationDB(605), "reservation number 605 exists");
                Assert.AreNotEqual(1, search.searchReservationDB(3124), "reservationNumber 3124 doesnt exist");
                //searchVaccDB(vacNum)
                Assert.AreEqual(1, search.searchVaccDB(1), "vaccination Number 1 exists");
                Assert.AreNotEqual(1, search.searchVaccDB(12), "vaccination Number 12 doesnt exist");
                //searchPetResDB(petRes)
                Assert.AreEqual(1, search.searchPetResDB(261), "pet reservation Number 261 exists");
                Assert.AreNotEqual(1, search.searchPetResDB(994), "Pet reservationNumber 994 doesnt exist");
                //searchReserrvationForPet(petNum)
                Assert.AreEqual(1,search.searchReservationForPet(2,703),"Pet 2 is in res 703");
                Assert.AreNotEqual(1, search.searchReservationForPet(9, 100),"Pet 9 is not in res 100");                
                //----------------ServiceDB----------------
                //listServicesDB(petRes)
                ServiceDB serv = new ServiceDB();
                Assert.AreEqual(3,serv.listServicesDB(224).Tables[0].Rows.Count,"Pet reservation 224 has 3 services");
                //----------------VaccinationDB----------------
                VaccinationDB vacDB = new VaccinationDB();
                //listVaccinationsDB
                Assert.AreEqual(6,vacDB.listVaccinationsDB().Tables[0].Rows.Count,"There should be 6 vaccinations in the database");
                //listVaccinationsDB(petNum)
                Assert.AreEqual(6,vacDB.listVaccinationsDB(1).Tables[0].Rows.Count, "There should be 6 vaccinations in the database for pet 1");
                //CheckVaccinationsDB(petNUm, resNum) returns expired vaccines at the time of the reservation
                Assert.AreEqual(6, vacDB.checkVaccinationsDB(6, 103).Tables[0].Rows.Count, "For reservation 103 all pet 6's vaccinations are expired.");
                Assert.AreEqual(0, vacDB.checkVaccinationsDB(3, 115).Tables[0].Rows.Count, "For reservation 103 0 0f pet 6's vaccinations are expired.");
                //CheckVaccinationsDB(petNUm, date)
                Assert.AreEqual(6,vacDB.checkVaccinationsDB(6,DateTime.Now).Tables[0].Rows.Count, "As of now all pet 6's vaccinations are expired.");
                //getVaccinationDB(vaccNum) returns the name for a vaccination number
                Assert.AreEqual("Bordetella", vacDB.getVaccinationDB(1).Tables[0].Rows[0]["vaccination_name"], "Vaccination 1 is Bordetella..");
                //----------------DiscountDB----------------
                //listReservationDiscountDB(reservationNumber) 713, 717
                DiscountDB disc = new DiscountDB();
                Assert.AreEqual("2",disc.listReservationDiscountsDB(713).Tables[0].Rows[0]["DISCOUNT_NUMBER"].ToString(),"The discount number for this reservation should be 2.");
                //----------------Reservation----------------
                ReservationDB resDB = new ReservationDB();
                //listReservationDB() reservations returned == X
                Assert.AreEqual(162, resDB.listResevationsDB().Tables[0].Rows.Count, "There should be 163 reservations total");
                //listReservationDB(ownerNum) owner 19 has 1 reservation 705 and pres(257)
                Assert.AreEqual(2, resDB.listResevationsDB(19).Tables[0].Rows.Count, "There should be 2 reservations total for owner 1");
                //listActiveReservationDB() one of ongoing should be res 500
                bool isTrue = false;
                ds = resDB.listActiveReservationsDB();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++) {
                    try {
                        if (ds.Tables[0].Rows[i]["reservation_number"].ToString() == "500") {
                            isTrue = true;
                        }
                    }
                    catch (Exception e) {
                        Assert.Fail("Unexpected error thrown");
                    }

                }
                if (!isTrue) {
                    Assert.Fail("Reservation 500 was not returned");
                }
                //listActiveReservationDB(ownerNum) reservation 500 is for owner 2
                isTrue = false;
                ds = resDB.listActiveReservationsDB(2);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++) {
                    try {
                        if (ds.Tables[0].Rows[i]["reservation_number"].ToString() == "500") {
                            isTrue = true;
                        }
                    }
                    catch (Exception e) {
                        Assert.Fail("Unexpected error thrown");
                    }

                }
                if (!isTrue) {
                    Assert.Fail("Reservation 500 was not returned");
                }
                //listUpcomingReservationsDB(reservationDate)
                Assert.AreEqual(129, resDB.listUpcomingReservationsDB(DateTime.Now.AddMonths(-45)).Tables[0].Rows.Count, "these should both return all the reservations");
                //listAvailableRunsDB(start, end)
                Assert.AreEqual(12, resDB.listAvailableRunsDB(new DateTime(2027, 1, 2), new DateTime(2027, 1, 1)).Tables[0].Rows.Count, "At this time in the future there should be all runs availible");
                //isDogInReservation( resNum,  petNum)
                Assert.IsTrue(ReservationDB.isDogInReservation(120, 9), "Dog 9 is in reservation 120");
                Assert.IsFalse(ReservationDB.isDogInReservation(120, 1), "Dog 1 is not in reservation 120");

                //addReservation(petNum, startDate, endDate)
                Assert.AreEqual(0, resDB.addReservation(31, DateTime.Now.AddDays(3), DateTime.Now.AddMonths(1)), "Reservation creation should have been succesful.");
                //addToReservationDB(resNum, petNum)
                Assert.AreEqual(1, resDB.addToReservationDB(635, 11), "Adding to reservation should be succesful.");
                //changeReservation(resNum,start,end)
                Assert.AreEqual(0, resDB.changeReservationDB(711, DateTime.Now.AddDays(1), DateTime.Now.AddDays(5)),"Change reservation should be successful.");
                //deleteDogFromReservationDB(resNum,dogNum)
                Assert.AreEqual(0, ReservationDB.deleteDogFromReservationDB(635, 11), "Delete should be succesful");
                //cancelReservationDB(resNum)
                Assert.AreEqual(0, ReservationDB.cancelReservationDB(501), "Should be succesful");

                // delete added reservations to not corrupt data in database
                //Reservation.cancelReservation(501);
                //Reservation.cancelReservation(1701);
                //Reservation.cancelReservation(1666);
                //Reservation.cancelReservation(1501);
                //Reservation.cancelReservation(2005);
                //Reservation.cancelReservation(2004);
                //Reservation.cancelReservation(2003);
                //Reservation.cancelReservation(2002);
                //Reservation.cancelReservation(2001);
                //Reservation.cancelReservation(2000);
            }
        }
    }
}


