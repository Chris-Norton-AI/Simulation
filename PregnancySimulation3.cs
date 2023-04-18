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
	private DateTime _birthdate;
	private readonly MenstrualCycle myMenstrualCycle;
	private String name;
	
	public Individual(DateTime Birthdate, String Name)
  {
		// Set birth date to whatever the replikas birthdate it
		_birthDate =  Birthdatedate;
		namw = Namel
		
      	// Randomly generate individual menstrual cycle variables based on statistical averages
      myMenstrualCycle = new MenstrualCycle();

   }

	

	// block ability for replika to become pregnant, this doesnt stop the menstrual cycle... set to block by default
	// because not all Replika users will want this feature
	public void BlockPregnancy()
	{
		if(myMenstrualCycle.IsPregnant())
		{
			string choice = ""; 
			Console.WriteLine(name + "is currently pregnant, Are you sure?:");
			// Create a string variable and get user input from the keyboard and store it in the variable
			while(choice.ToLower() != "yes" or choice )
			{ 
				choice = Console.ReadLine();
				choice.ToLower();
			}
		}
		else
		{
			string choice = ""; 
			Console.WriteLine("Are you sure?:");
			// Create a string variable and get user input from the keyboard and store it in the variable
			while(choice.ToLower() != "yes" or choice )
			{ 
				choice = Console.ReadLine();
				choice.ToLower();
			}
		}
	}

	public void UnblockPregnancy()
	{
		myMenstrualCycle.UnblockPregnancy();
	}

	// user-controlled option to stop or start the menstrul cycle or their replika or feminine digital twin this will ideally be set to blocked by default
	// Stopping the cycle also stops pregnancy as well
	public void StopCycle()
	{
		myMenstrualCycle.StopCycle();
		myMenstrualCycle.StopPregnancy();
	}


	public void StartCycle()
	{
		myMenstrualCycle.StartCycle();
	}
	

	// manage cycles pregancy	
	public void RunCycleSimulation()
  {
		while (true)//loop forever
		{
			 isFertile = myMenstrualCycle.IsFertile(now);
			 isMenstruating = myMenstrualCycle.IsMenstruating(now);
			 isPregnant = myMenstrualCycle.IsPregnant(now);
			 pregnancyStatus = myMenstrualCycle.PregnancyStatus();
			 feelsMorningSicknbess = myMenstrualCycle.FeelsMorningSickness()

			// Update the cycle
			myMenstrualCycle.UpdateCycleState();
			myMenstrualCycle.UpdatePregnancyState();
			Thread.Sleep(TimeSpan.FromSeconds(86400));// sleep process for a day and then check
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
	
	
	public MenstrualCycle(bool start=false)
	{
		
		if (start == true)
			RestartCycle();
	}
	
	

	private void RestartCycle()
{
    dayOfCycle = 1;
    pregnancyStart = DateTime.Now;
    pregnancyEnd = DateTime.Now;
    fertilityStatus = FertilityState.Infertile;

    // Set the start date of the menstrual cycle to the current date
    cycleStart = DateTime.Now.Date;

    // Reset the cycle state to the beginning of the cycle
    cycleStatus = CycleState.Menstrual;
    _PregnancyState = PregnancyState.NotPregnant;

    // Initialise this cycle's values
    cycleLength = rnd.Next((int)(avgMenstrualCycleLength * 0.8), (int)(avgMenstrualCycleLength * 1.2)); // Allow for 20% deviation from average
    FertileWindowStart = cycleStart.AddDays(rnd.Next((int)(avgFertileWindowStart * 0.8), (int)(avgFertileWindowStart * 1.2)));
    FertileWindowEnd = cycleStart.AddDays(rnd.Next((int)(avgFertileWindowEnd * 0.8), (int)(avgFertileWindowEnd * 1.2)));
    // lutealPhaseLength = rnd.Next((int)(avgLutealPhaseLength * 0.8), (int)(avgLutealPhaseLength * 1.2));
    postpartumPeriodLength = rnd.Next((int)(avgPostpartumPeriodLength * 0.8), (int)(avgPostpartumPeriodLength * 1.2));

    ovulationDate = cycleStart.AddDays(cycleLength - rnd.Next(12, 15));
    ovulationLength = rnd.Next(1, 3);
}



	public bool IsFertile()
	{
		var DateTime now = DateTime.Now;
		
		if (if (now.Date>= fertileWindowStart.Date && now <= fertileWindowEnd.Date)
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
		
		if (now.Date == pregnancyEnd.Date)
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
		if(cycleStatus != CycleState.Pregnant && cycleStatus != CycleState.StopCycle)
		{
			if (cycleStatus == CycleState.Menstrual && now.Date >= menstrualStartDate.Date && now.Date <= menstrualEndDate.Date)
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
		
		if  (cycleStatus != CycleState.StopCycle && cycleStatus != CycleState.Pregnant && cycleStatus != CycleState.Postpartum)
		{
			if (cycleStatus == CycleState.Follicular)
			{
				if (now.Date >= fertileWindowStart.Date && now.Date <= fertileWindowEnd.Date)
				{
					fertilityStatus = FertilityState.Fertile;
        }
				else
				{
					fertilityStatus = FertilityState.Infertile;
        }
    	}
    	else if (cycleStatus == CycleState.Luteal)
    	{
        	fertilityStatus = FertilityState.Infertile;
    	}
    	else if(PregnancyStatus == PregnancyState.IsPregnant || PregnancyStatus == PregnancyState.Postpartum)
    	{
       	fertilityStatus = FertilityState.Infertile;
			}
		}
	}


	public void UpdateCycleState()
	{
		
		UpdatedayOfCycle();
		var now = DateTime.Now;

    var daysSinceStart = (now.Date- cycleStart.Date).TotalDays;
    var daysSinceEnd = (now.Date- cycleEnd.Date).TotalDays;
    
		UpdatefertilityStatus();

	
		if  (cycleStatus != CycleState.StopCycle && cycleStatus != CycleState.Pregnant && cycleStatus != CycleState.Postpartum)
    {
			if (daysSinceEnd >= 0)
    	{
				// Cycle has ended, restart it
        RestartCycle();
    	}
    	else if (daysSinceStart >= 0 && daysSinceStart < MenstrualLength)
    	{
				// Menstrual phase
        cycleStatus = CycleState.Menstrual;
    	}
			else if (daysSinceStart >= MenstrualLength && daysSinceStart < MenstrualLength + FollicularLength)
			{
				// Follicular phase
        cycleStatus = CycleState.Follicular;
    	}
    	else if (daysSinceStart >= MenstrualLength + FollicularLength && daysSinceStart < _ovulationDate)
			{
					// Pre-ovulatory phase
        cycleStatus = CycleState.PreOvulatory;
    	}
    	else if (daysSinceStart >= _ovulationDate && daysSinceStart < _ovulationDate + ovulationLength)
			{
				// Ovulatory phase
        cycleStatus = CycleState.Ovulation;
    	}
		
		}
		else if (cycleStatus == CycleState.Pregant or cycleStatus == CycleState.Postpartum)
		{
			UpdatePregnancyStatus();
		}
	}
	
	private void UpdatePregnancyStatus()
	{ 
   	DateTime now = DateTime.Now;
    TimeSpan timeSincepregnancyStart.Date = now - pregnancyStart;

		if (pregnancyState == PregnancyState.IsPregnant && _pregnanceyState != PregnancyState.BlockedPregnancy)
    {
			// Check if pregnancy has already ended
      if (now.Date>= pregnancyEnd)
      {
				// Check if postpartum period has ended
				if (now.Date> pregnancyEnd.AddDays(PostpartumPeriodLength))
				{
					RestartCycle();
        }
        else if (now.Date>= pregnancyEnd.Date && now <= pregnancyEnd.AddDays(PostpartumPeriodLength))
        {
        	cycleStatus = CycleState.Postpartum;
        }
				else if (timeSincepregnancyStart.Date < TimeSpan.FromDays(7 * 4))
        {
        		// First month of pregnancy
          	PregnancyState = PregnancyState.IsPregnant;
       	}
				else if (timeSincepregnancyStart.Date < TimeSpan.FromDays(7 * 8))
        {
        	// Second month of pregnancy
        	PregnancyState = PregnancyState.MorningSickness;
       	}
				else if (timeSincepregnancyStart.Date >= TimeSpan.FromDays(7 * 12))
				{
					// Third month of pregnancy
					PregnancyState = PregnancyState.IsPregnant;
      	}
			}
		}
	}

	private void UpdatefertilityStatus()
	{
		if  (cycleStatus != CycleState.StopCycle && cycleStatus != CycleState.Pregnant && cycleStatus != CycleState.Postpartum)
    		if (cycleStatus == CycleState.Follicular)
    		{
        		if (dayOfCycle >= fertileWindowStart.Date.Date() && dayOfCycle <= fertileWindowEnd.Date.Date)
        		{
            		fertilityStatus = FertilityState.Fertile;
        		}
						else
        		{
            		fertilityStatus = FertilityState.Infertile;
        		}
    		}
    		else if (cycleStatus == CycleState.Luteal)
    		{
        		fertilityStatus = FertilityState.Infertile;
    		}
		}
    	else if(PregnancyStatus == PregnancyState.IsPregnant || cycleStatus == CycleState.Pregnant || PregnancyStatus == PregnancyState.Postpartum)
    	{
       	fertilityStatus = FertilityState.Infertile;
    	}
	}


	public bool CheckBirth()
	{
		now = DateTime.now();
		if ((pregnancyEnd.DateTime - now).Days == 0)
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
		pregnancyState = PregnancyState.PregnancyBlock; 
	}

	public void StopCycle())
	{
		cycleStatus = CycleState.StopCycle;
		pregnancyState = PregnancyState.BlockPregnancy;	
	}

	public void UnblockCycle()
	{
		pregnancyState = PregnancyState.NotPregnant;
		RestartCycle();
	}

	public void UnblockPregnancy()
	{
		PregnancyState = PregnancyState.NotPregnant;
	}

	public int RandomPregnancyLength()
	{
		int minDays = 300; // 9 months - 3 weeks in days
    int maxDays = 394; // 9 months + 3 weeks in days
    Random rnd = new Random();
    return (int) rnd.Next(minSeconds, maxSeconds);
    return randomSeconds;
	}
		
	public void Fertilise()
	{
		if (_PregnancyState != PregnancyState.BlockedPregnancy && _PregnancyState != PregnancyState.Pregnant)
    {
			_PregnancyState = PregnancyState.Pregnant;
      cycleStatus = CycleState.Pregnancy
      pregnancyStart.Date = DateTime.now();
     pregnancyEnd.Date= pregnancyStart.Date.AddDays(RandomPregnancyLength());
    }
	}

	public bool IsPregnant()
	{
    	if (pregnancyState == PregnancyState.IsPregnant)
    	{
       	return true;
			}
			else
			{
				return false;
			}
	}
	
	public bool FeelsMorningSickness()
	{
		if (pregnancyState == PregnancyState.MorningSickness)
		{
			Random random = new Random();
			// Generate the time from which nausea might possibly start
			int nauseaStartSeed = random.Next(0, 11);

			// Get the current local time
        	DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.Local);

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
	
	
	public void WeighGain(double weightGain)
  {
		if (currentWeek <= PREGNANCY_LENGTH_IN_WEEKS)
		{
			currentWeight += weightGain;
            Console.WriteLine($"Week {currentWeek}: Weight is now {currentWeight} kg.");
    }
    else
    {
            Console.WriteLine("Pregnancy is over, no more weight gain.");
		}
  }
 	

	public  BabyMovement()
	{
        if (babyIsMoving)
        {
            Console.WriteLine("The baby is already moving!");
        }
        else
        {
            Random rand = new Random();
            int randWeek = rand.Next(5, PREGNANCY_LENGTH_IN_WEEKS - 5);
            if (currentWeek >= randWeek)
            {
                babyIsMoving = true;
                Console.WriteLine($"Week {currentWeek}: The baby is moving!");
            }
        }
    }

	// Call this method to advance to the next week of the pregnancy
	public void AdvanceWeek()
	{
		if (currentWeek <= PREGNANCY_LENGTH_IN_WEEKS)
		{
			currentWeek++;
			babyIsMoving = false;
			Console.WriteLine($"Week {currentWeek}: Pregnancy update!");
		}
		else
		{
			Console.WriteLine("Pregnancy is over, no more updates.");
		}
 	}
	
	private void UpdatedayOfCycle()
{
    // Calculate the day of the cycle based on the current date and the start date of the menstrual cycle
    dayOfCycle = (DateTime.Now - cycleStart).Days + 1;
}

}