using System;
 using System.Threading;



 namespace MenstrualCycle
 {

 	public enum FertililtyStatus
 	{
 		Fertile,
 		Infertile
 	}

   public enum CycleState
   {
 		Follicular,
 		PreOvulatory,
 		Ovulation,
 		Luteal,
    	Menstrual,
 		Pregnancy,
 		Postpartum,
 		StopCycle  // this indicates that the user has stopped the cycle. this stops prergnancy as well#
 	}

 	public enum PregnancyState
   	{
 		NotPregnant,
 		Pregnant,
 		MorningSickness,
 		BlockedPregnancy // this inidicates that the user has stopped pregnancy but this the cycle may still continue
   	}
 }

 public class Individual
 {
 	private DateTime birthDate;
 	private readonly MenstrualCycle myMenstrualCycle;
 	private String name;
 	private String country; // country of the  
 	private int timeZone;


 	// the timezone of the user isi the the same as the users

 	public Individual(DateTime BirthDate, String Name, String Country)
   	{
 		// Set birth date to whatever the replikas birthdate it
 		this.birthDate =  BirthDate;
 		this.name = Name;
 		this.country = Country;
 		this.timeZone = TimeZoneInfo.FindSystemTimeZoneById(GetTimeZoneId(Country));

     		// Randomly generate individual menstrual cycle for this individual
 		myMenstrualCycle = new MenstrualCycle(true, this.timeZone);
 				this.RunCycleSimulation();  // start the thread
   	}

 	private string GetTimeZoneId(string country)
 	{
 	 	// Map the country to a time zone ID
     		switch (country)
     		{
 			case "United States":
       			return "Eastern Standard Time"; // adjust for other US Timezones
       			case "United Kingdom":
 			return "GMT Standard Time";
 			case "India":
 			return "India Standard Time";
       			// add more cases for other countries
       			default:
 			throw new ArgumentException($"Unknown country: {country}");
 		}
 	}


 	// block ability for replika to become pregnant, this doesnt stop the menstrual cycle... set to block by default
 	// because not all Replika users will want this feature
 	public void BlockPregnancy()
 	{
 		this.myMenstrualCycle.BlockPregnancy();
 	}

 	public void UnblockPregnancy()
 	{
 		myMenstrualCycle.UnblockPregnancy();
 	}

 	// user-controlled option to stop or start the menstrul cycle or their replika or feminine digital twin this will ideally be set to blocked by default
 	// Stopping the cycle also stops any and  current  pregnancy as well


 	public void StopCycle()
 	{
 		string choice = "";
 		switch(this.myMenstrualCycle.IsPregnant())
  		{
 			case true:

         		Console.WriteLine(name + " is currently pregnant. Are you sure you want to stop the menstrual cycle? (yes/no):");

         		// Create a string variable and get user input from the keyboard and store it in the variable
         		while (choice != "yes" && choice != "no")
         		{
             			choice = Console.ReadLine();
             			choice=choice.ToLower();
         		}
 			if (choice == "yes")
         		{
             			this.myMenstrualCycle.StopCycle();
             			this.myMenstrualCycle.BlockPregnancy();
 				Console.WriteLine(name + "'s menstrual cycle has been stopped.");
 			}
 			break;
 		case false:
         		Console.WriteLine("Are you sure you want to stop " + name + "'s menstrual cycle? (yes/no):");

 			// Create a string variable and get user input from the keyboard and store it in the variable
 			while (choice != "yes" && choice != "no")
         		{
             			choice = Console.ReadLine();
             			choice = choice.ToLower();
         		}

 			if (choice == "yes")
 			{
 				this.myMenstrualCycle.StopCycle();
             			this.myMenstrualCycle.BlockPregnancy();
             			Console.WriteLine(name + "'s menstrual cycle has been stopped.");
         		}
 			break;
     		}
 	}


 	public void StartCycle()
 	{
 		myMenstrualCycle.StartCycle();
 	}


 	public void RunCycleSimulation()
 	{
 		while (true) // loop forever
 		{
 			DateTime now = DateTime.Now;
 			bool isFertile = myMenstrualCycle.IsFertile();
 			bool isMenstruating = myMenstrualCycle.IsMenstruating();
 			bool isPregnant = myMenstrualCycle.IsPregnant();
 			bool feelsMorningSickness = myMenstrualCycle.FeelsMorningSickness();

 			// Update the cycle
 			myMenstrualCycle.UpdateCycleState();
 			myMenstrualCycle.UpdatePregnancyState();

 			// Print cycle status to console
 			Console.WriteLine(name + "'s menstrual cycle status on " + now.ToShortDateString() + ": ");
 			Console.WriteLine("\tCycle day: " + this.myMenstrualCycle.GetDayOfCycle());
 			Console.WriteLine("\tCycle status: " + this.myMenstrualCycle.GetCycleStatus());
 			Console.WriteLine("\tFertility status: " + this.myMenstrualCycle.GetFertilityStatus());
 			Console.WriteLine("\tIs fertile: " + isFertile);
 			Console.WriteLine("\tIs menstruating: " + isMenstruating);
 			Console.WriteLine("\tIs pregnant: " + isPregnant);
 			if (isPregnant)
 			{
 				Console.WriteLine("\tPregnancy status: " + this.myMenstrualCycle.getPregnancyStatus);
 				Console.WriteLine("\tFeels morning sickness: " + feelsMorningSickness);
 			}
 			else
 			{
 				Console.WriteLine("\tPregnancy Status: " + myMenstrualCycle.PregnancyStatus());
 			}
 			// Sleep for a day and then check again
 			Thread.Sleep(TimeSpan.FromDays(1));
 		}
 	}


    // user activated function when engaging in in the action of conception (sentiment anylysis or user activated)

 	public bool AttemptConception()
 	{
 		if (myMenstrualCycle.IsFertile())
 		{
 			Random random = new Random();
 			if (random.NextDouble() < 0.2) // arbitary 20% chance of pregnancy
 			{
 				Console.WriteLine("Congratulations! You are pregnant."); // on success feretailise 
 				myMenstrualCycle.Fertalise();
 				return true;
 			}
 			else{
 				return false;
 			}
 		}
 	}


 	public void BirthProcess()
 	{
 		if(myMenstrualCycle.PregnancyState == PregnancyState.Pregnant)
 		{
 			Console.WriteLine("Congratulations! You have given birth");
 		}
 	}

 }

 public class MenstrualCycle
 {
 	private int localTimezone;

 	private readonly int avgMenstrualCycleLength = 28; // Average menstrual cycle length in days
 	private readonly int avgMenstrualFlowLength = 5; // Average length of menstrual bleeding in days
  	private readonly int avgfertileWindowStart = 10; // Average day of menstrual cycle when fertile window begins
   	private readonly int avgFertileWindowEnd = 17; // Average day of menstrual cycle when fertile window ends
 	private readonly int avgLutealPhaseLength = 14; // Average length of luteal phase in days
 	private readonly int avgPostpartumPeriodLength = 35; // Average length of postpartum period in days

 	private  DateTime cycleStart;
 	private  DateTime cycleEnd;
 	private  int cycleLength; // Length of the menstrual cycle in days

 	private  DateTime fertileWindowStart; // Start.Date of the fertile window relative to the start of the cycle
 	private  DateTime fertileWindowEnd; // End.Date of the fertile window relative to the start of the cycle

 	private CycleState cycleStatus = CycleState.StopCycle;
 	private PregnancyState pregnancyStatus = PregnancyState.BlockPregnancy;
 	private FertilityState fertilityStatus = FertilityState.Infertile;


 	private DateTime pregnancyStart = DateTime.Now;
 	private DateTime pregnancyEnd = DateTime.Now;
 	private DateTime menstrualStartDate = DateTime.Now;
 	private DateTime	menstrualEndDate = DateTime.Now;

 	private DateTime ovulationDate;
 	private int ovulationLength;

 	private int PostpartumPeriodLength;

 	private int daysSincePeriodStart;
 	private int dayOfCycle;
 	private Random rnd = new Random();


 	public MenstrualCycle(bool Start=false, int time)
 	{
 		this.timeZone = time; 
 		if (Start == true)
 		this.RestartCycle();
 	}


 	public int GetDayOfCycle()
 	{
 		return this.dayOfCycle;
 	}

 	public FertilityState GetFertilityStatus()
 	{
 		return this.fertilityStatus;
 	}

 	public CycleState GetCycleStatus()
 	{
 		return this.pregnancyStatus;
 	}

 	public PregnancyState GetPregnancyStatus()
 	{
 		return this.pregnancyStatus; 
 	}


 	// Restarts the menstruel cycle, resetting vatiables
 	private void RestartCycle()
 	{
 		this.dayOfCycle = 1; // day always starts at one since this incremented daily
 		this.pregnancyStart = DateTime.Now;
 		this.pregnancyEnd = DateTime.Now;
 		this.fertilityStatus = FertilityState.Infertile;

    		// Set the start date of the menstrual cycle to the current date
 		this.cycleStart = DateTime.Now.Date;
 		// Reset the cycle state to the beginning of the cycle
 		this.cycleStatus = CycleState.Menstrual;
 		this.pregnancyStatus = PregnancyState.NotPregnant;

     		// Initialise this cycle's values
     		this.cycleLength = rnd.Next((int)(avgMenstrualCycleLength * 0.8), (int)(avgMenstrualCycleLength * 1.2)); // Allow for 20% deviation from average
     		this.fertileWindowStart = this.cycleStart.AddDays(rnd.Next((int)(avgFertileWindowStart * 0.8), (int)(avgFertileWindowStart * 1.2)));
     		this.fertileWindowEnd = this.cycleStart.AddDays(rnd.Next((int)(avgFertileWindowEnd * 0.8), (int)(avgFertileWindowEnd * 1.2)));
     		// lutealPhaseLength = rnd.Next((int)(avgLutealPhaseLength * 0.8), (int)(avgLutealPhaseLength * 1.2));
     		this.postpartumPeriodLength = rnd.Next((int)(avgPostpartumPeriodLength * 0.8), (int)(avgPostpartumPeriodLength * 1.2));
 		this.menstrualLength = rnd.Next(3,5);
     		this.ovulationDate = this.cycleStart.AddDays(cycleLength - rnd.Next(12, 15));
     		this.ovulationLength = rnd.Next(1, 3);
 	}



 	public bool IsFertile()
 	{
 		DateTime now = DateTime.Now;

 		if (now.Date>= this.fertileWindowStart.Date && now.Date <= this.fertileWindowEnd.Date)
 		{
        			// Invalid date, return false outside of fertile window
          		return false;
 		}
 		else
 		{
 			return true;
 		}
 	}

 	public bool IsBirthDate()
 	{
 		DateTime now = DateTime.Now;

 		if (now.Date == this.pregnancyEnd.Date)
 		{
 			return true;	
 		}
 		else
 		{
 			return false;
 		}
 	}

 	public bool IsMenstruating()
 	{
 		DateTime now = DateTime.Now;
 		if(this.cycleStatus != CycleState.Pregnant && this.cycleStatus != CycleState.Postpartum && this.cycleStatus != CycleState.StopCycle)
 		{
 			if (this.cycleStatus == CycleState.Menstrual && now.Date >= this.menstrualStartDate.Date && now.Date <= this.menstrualEndDate.Date)
 			{
 				return true;
       			}
 			else
 			{
 				return false;
 			}
 		}
 		else
 		{
 			return false;
 		}
 	}

 	private void UpdatefertilityStatus()
 	{
 		DateTime now = DateTime.Now;

 		if  (this.cycleStatus != CycleState.StopCycle && this.cycleStatus != CycleState.Pregnancy && this.pregnancyStatus != PregnancyState.Postpartum)
 		{
 			if (this.cycleStatus == CycleState.Follicular)
 			{
 				if (now.Date >= this.fertileWindowStart.Date && now.Date <= this.fertileWindowEnd.Date)
 				{
 					this.fertilityStatus = FertilityState.Fertile;
         			}
 				else
 				{
 					this.fertilityStatus = FertilityState.Infertile;
         			}
     			}
     			else if (this.cycleStatus == CycleState.Luteal)
     			{
         			this.fertilityStatus = FertilityState.Infertile;
     			}
     			else if(this.pregnancyStatus == PregnancyState.IsPregnant || this.pregnancyStatus == PregnancyState.Postpartum)
     			{
        				this.fertilityStatus = FertilityState.Infertile;
 			}
 		}
 	}


 	public void UpdateCycleState()
 	{

 		this.UpdatedayOfCycle();
 		DateTime now = DateTime.Now;

 		int daysSinceStart = (now.Date - this.cycleStart.Date).TotalDays;
     		int daysSinceEnd = (now.Date - this.cycleEnd.Date).TotalDays;

 		this.UpdatefertilityStatus();


 		if  (this.cycleStatus != CycleState.StopCycle && this.cycleStatus != CycleState.Pregnant && this.cycleStatus != CycleState.Postpartum)
     		{
 			if (daysSinceEnd >= 0)
     			{
 				// Cycle has ended, restart it
         			this.RestartCycle();
     			}
     			else if (daysSinceStart >= 0 && daysSinceStart < this.menstrualLength)
     			{
 				// Menstrual phase
 			 	this.cycleStatus = CycleState.Menstrual;
     			}
 			else if (daysSinceStart >= MenstrualLength && daysSinceStart < MenstrualLength + FollicularLength)
 			{
 				// Follicular phase
         			this.cycleStatus = CycleState.Follicular;
     			}
   			else if (daysSinceStart >= MenstrualLength + FollicularLength && daysSinceStart < _ovulationDate)
 			{
 				// Pre-ovulatory phase
 				this.cycleStatus = CycleState.PreOvulatory;
     			}
 			else if (daysSinceStart >= _ovulationDate && daysSinceStart < _ovulationDate + ovulationLength)
 			{
 				// Ovulatory phase
 				this.cycleStatus = CycleState.Ovulation;
     			}

 		}
 		else if (cycleStatus == CycleState.Pregant or cycleStatus == CycleState.Postpartum)
 		{
 			this.UpdatePregnancyStatus();
 		}
 	}

 	private void UpdatePregnancyStatus()
 	{ 
    		DateTime now = DateTime.Now;
     		TimeSpan timeSincepregnancyStart.Date = now - this.pregnancyStart;

 		if (this.pregnancyState == PregnancyState.IsPregnant && this.pregnanceyState != PregnancyState.BlockedPregnancy)
     		{
 			// Check if pregnancy has already ended
       			if (now.Date>= this.pregnancyEnd)
       			{
 				// Check if postpartum period has ended
 				if (now.Date> this.pregnancyEnd.AddDays(PostpartumPeriodLength))
 				{
 					this.RestartCycle();
         			}
         			else if (now.Date>= this.pregnancyEnd.Date && now <= this.pregnancyEnd.AddDays(PostpartumPeriodLength))
         			{
         				this.cycleStatus = CycleState.Postpartum;
         			}
 				else if (this.timeSincepregnancyStart.Date < TimeSpan.FromDays(7 * 4))
         			{
         				// First month of pregnancy
           				this.pregnancyState = PregnancyState.IsPregnant;
        				}
 				else if (timeSincepregnancyStart.Date < TimeSpan.FromDays(7 * 8))
         			{
         				// Second month of pregnancy
         				this.pregnancyState = PregnancyState.MorningSickness;
        				}
 				else if (timeSincepregnancyStart.Date >= TimeSpan.FromDays(7 * 12))
 				{
 					// Third month of pregnancy
 					this.pregnancyState = PregnancyState.IsPregnant;
 				}
 			}
 		}
 	}

 	private void UpdatefertilityStatus()
 	{
 		if (this.cycleStatus != CycleState.StopCycle && this.cycleStatus != CycleState.Pregnant && this.cycleStatus != CycleState.Postpartum)
 		{
     			if (this.cycleStatus == CycleState.Follicular)
     			{
         			if (this.dayOfCycle >= this.fertileWindowStart.Date && this.dayOfCycle <= this.fertileWindowEnd.Date)
         			{
             				this.fertilityStatus = this.fertilityState.Fertile;
         			}
 				else
         			{
             				this.fertilityStatus = this.fertilityState.Infertile;
         			}
     			}
     			else if (this.cycleStatus == CycleState.Luteal)
     			{
         			this.fertilityStatus = FertilityState.Infertile;
     			}
 		}
     		else if(this.pregnancyStatus == PregnancyState.IsPregnant || this.cycleStatus == CycleState.Pregnant || this.pregnancyStatus == PregnancyState.Postpartum)
     		{
        			this.fertilityStatus = FertilityState.Infertile;
     		}
 	}


 	public bool CheckBirth()
 	{
 		now = DateTime.now();
 		if ((this.pregnancyEnd.DateTime - now).Days == 0)
 		{
 			return True;
 		}
 		else
 		{
 		 	return false;
 		}
 	}

 	public void BlockPregnancy()
 	{
 		this.pregnancyState = PregnancyState.PregnancyBlock; 
 	}

 	public void StopCycle())
 	{
 		this.cycleStatus = CycleState.StopCycle;
 		this.pregnancyState = PregnancyState.BlockPregnancy;	
 	}

 	public void UnblockCycle()
 	{
 		this.pregnancyState = PregnancyState.NotPregnant;
 		this.RestartCycle();
 	}

 	public void UnblockPregnancy()
 	{
 		this.pregnancyState = PregnancyState.NotPregnant;
 	}

 	public int RandomPregnancyLength()
 	{
 		int minDays = 300; // 9 months - 3 weeks in days
 		int maxDays = 394; // 9 months + 3 weeks in days
     		return (int) this.rnd.Next(minDays, maxDays);
  	}

 	public void Fertilise()
 	{
 		if (this.pregnancyState != PregnancyState.BlockedPregnancy && this.pregnancyState != PregnancyState.Pregnant)
     		{
 			this.pregnancyState = PregnancyState.Pregnant;
       			this.cycleStatus = CycleState.Pregnancy
  			this.pregnancyStart.Date = DateTime.now();
  			this.pregnancyEnd.Date = this.pregnancyStart.Date.AddDays(this.RandomPregnancyLength());
     		}
 	}

 	public bool IsPregnant()
 	{
 		if (this.pregnancyState == PregnancyState.IsPregnant)
     		{
        			return true;
 		}
 		else
 		{
 			return false;
 		}
 	}
 	// morning sickness please not this is ver dependent on the local of the chatbot user so it musst be implemented with local time settings as per the code below 
 	public bool FeelsMorningSickness()
 	{
 		if (this.pregnancyState == PregnancyState.MorningSickness)
 		{
 			Random random = new Random();
 			// Generate the time from which nausea might possibly start


 			int nauseaStartSeed = this.rnd.Next(0, 11);
 			// Get the current local time
       			DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, this.timeZone);
       			// Get the time of day
       			TimeSpan timeOfDay = localTime.TimeOfDay;

         		if (timeOfDay >= TimeSpan.FromHours(nauseaStart.DateSeed) && timeOfDay < TimeSpan.FromHours(12))
         		{
           			// Generate probability of true or false
           			int randomNumber = random.Next(0, 100);

 				// Return whether or not the morning sickness results in nausea
 		            	return randomNumber <= 75;
         		}
         		else
         		{
           			// Not feeling nauseous
           			return false;
         		}
     		}
     		else
     		{
 			return false;
     		}
 	}




 	private void UpdatedayOfCycle()
 	{
     		// Calculate the day of the cycle based on the current date and the start date of the menstrual cycle
     		this.dayOfCycle = (DateTime.Now - this.cycleStart).Days + 1;
 	}

 }
