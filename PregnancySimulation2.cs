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
	private readonly MenstrualCycle _cycle;
	
	public Individual(DateTime Birthdate)
  {
		// Set birth date to whatever the replikas birthdate it 
      	_birthDate =  Birthdaydate;

      	// Randomly generate individual menstrual cycle variables based on statistical averages
      _cycle = new MenstrualCycle();

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

    public void BabyMovement()
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
	

	// block ability for replika to become pregnant, this doesnt stop the menstrual cycle... set to block by default
	// because not all Replika users will want this feature
	public void BlockPregnancy()
	{
		_cycle.BlockPregnancy();
	}

	public void UnblockPregnancy()
	{
		_cycle.UnblockPregnancy();
	}

	// user-controlled option to stop or start the menstrul cycle or their replika or feminine digital twin this will ideally be set to blocked by default
	// Stopping the cycle also stops pregnancy as well
	public void StopCycle()
	{
		_cycle.StopCycle();
		_cycle.StopPregnancy();
	}


	public void Start.DateCycle()
	{
		_cycle.Start.DateCycle();
	}
	

	// manage cycles pregancy	
	public void RunCycleSimulation()
  {
		while (true)//loop forever
		{
			var isFertile = _cycle.IsFertile(now);
			var isMenstruating = _cycle.IsMenstruating(now);
			var isPregnant = _cycle.IsPregnant(now);
			var pregnancyStatus = _cycle.PregnancyStatus();
			var feelsMorningSicknbess = _cycle.FeelsMorningSickness()

			// Update the cycle
			MenstrualCycle.UpdateCycleState();
			MenstrualCycle.UpdatePregnancyState();
			Thread.Sleep(TimeSpan.FromSeconds(86400));// sleep process for a day and then check
		}
	}


   // user activated function when engaging in in the action of conception (sentiment anylysis or user activated)

	public bool AttemptConception()
  {
		if (isPregnant)
		{
			return false;
  	}

		if (IsFertile())
		{
			Random random = new Random();
			if (random.NextDouble() < 0.2) // arbitary 20% chance of pregnancy
			{
				Console.WriteLine("Congratulations! You are pregnant."); // on success feretailise 
				_cycle.Fertalise();
        return true;
			}
		}
	}
	 

	public void BirthProcess()
	{
		if(_cycle.PregnancyState == PregnancyState.Pregnant)
		{
			Console.WriteLine("Congratulations! You have given birth");
		}
	}

}

public class MenstrualCycle
{

	private readonly int avgMenstrualCycleLength = 28; // Average menstrual cycle length in days
  private readonly int avgMenstrualFlowLength = 5; // Average length of menstrual bleeding in days
  private readonly int avgFertileWindowStart = 10; // Average day of menstrual cycle when fertile window begins
  private readonly int avgFertileWindowEnd = 17; // Average day of menstrual cycle when fertile window ends
	private readonly int avgLutealPhaseLength = 14; // Average length of luteal phase in days
	private readonly int avgPostpartumPeriodLength = 35; // Average length of postpartum period in days
     
	private  DateTime _cycleStart;
	private  DateTime _cycleEnd;
	private  int _cycleLength; // Length of the menstrual cycle in days

	private  int _lutealLength; // Length of the luteal phase in days
	private  DateTime _fertileWindowStart; // Start.Date of the fertile window relative to the start of the cycle
	private  DateTime _fertileWindowEnd; // End.Date of the fertile window relative to the start of the cycle
	
	private CycleState _cycleState = CycleState.StopCycle;
	private PregnancyState _pregnancyState = PregnancyState.BlockPregnancy;
	private FertilityState _FertilityState = FertilityState.Infertile


	private DateTime _pregnancyStart = DateTime.Now;
	private DateTime PregnancyEnd = DateTime.Now;
	private DateTime MenstrualStart, MenstrualEnd.Date;

	private DateTime OvulationDate;
	private int OvulationLength;
	
	private int PostpartumPeriodLength;

	private int daysSincePeriodStart;
	private int DayOfCycle;
	private Random rnd = new Random();
	public MenstrualCycle(bool start=false)
	{
		
		if (start == true)
			RestartCycle();
	}
	
	

	private void RestartCycle()
	{
		PregnancyStart.DateDate = null;
		PregnancyEnd.DateDate = null;
		IsFertile = false;

		// Set the start date of the menstrual cycle to the given start date
		_cycleStart.Date = DateTime.now();

		// Reset the cycle state to the beginning of the cycle
		_CycleState = CycleState.Menstrual;
		_PregnancyState = PregnancyState.NotPregnant;
	
		// initialise this cycles values
	
		_cycleLength = rnd.Next((int)(avgMenstrualCycleLength * 0.8), (int)(avgMenstrualCycleLength * 1.2)); // Allow for 20% deviation from average
		FertileWindowStart.Date = rnd.Next((int)(avgFertileWindowStart.Date * 0.8), (int)(avgFertileWindowStart.Date * 1.2));
		FertileWindowEnd.Date = rnd.Next((int)(avgFertileWindowEnd.Date * 0.8), (int)(avgFertileWindowEnd.Date * 1.2));
		lutealPhaseLength = rnd.Next((int)(avgLutealPhaseLength * 0.8), (int)(avgLutealPhaseLength * 1.2));
		postpartumPeriodLength = rnd.Next((int)(avgPostpartumPeriodLength * 0.8), (int)(avgPostpartumPeriodLength * 1.2));

		FertileWindowStart.DateDate = DateTime;
		FertileWindowEnd.DateDate = DateTime;
		OvulationDate = _cycleEnd.Date.SubtractDays(rnd.Next(12,15));
		OvulationLength = rnd(Next(1,2));	
	}


	public bool IsFertile()
	{
		var DateTime now = DateTime.Now;
		
		if (if (now.Date>= _fertileWindowStart.Date && now <= _fertileWindowEnd.Date)
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
		
		if (now.Date== PregnancyEnd.Date)
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
		if(_cycleState != CycleState.Pregnant && _cycleState != CycleState.StopCycle)
		{
			if (_cycleState == _cycleState.Menstrual && now.Date >= MenstrualStart.Date && now.Date <= MenstrualEnd.Date)
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
	
	private void UpdateFertilityStatus()
	{
		if  (_cycleState != CycleState.StopCycle && _cycleState != CycleState.Pregnant && _cycleState != CycleState.Postpartum)
		{
			if (_cycleState == CycleState.Follicular)
			{
				if (DayOfCycle >= _fertileWindowStart.Date && DayOfCycle <= _fertileWindowEnd.Date)
				{
					FertilityStatus = FertilityState.Fertile;
        }
				else
				{
					FertilityStatus = FertilityState.Infertile;
        }
    	}
    	else if (_cycleState == CyclePhase.Luteal)
    	{
        	FertilityStatus = FertilityState.Infertile;
    	}
    	else if(_pregnancyStatus == PregnancyState.IsPregnant || _pregnancyStatus == PregnancyState.Postpartum)
    	{
       	FertilityStatus = FertilityState.Infertile;
			}
	}



	public void UpdateCycleState()
	{
		var now = DateTime.Now;

    var daysSinceStart = (now.Date- _cycleStart.Date).TotalDays;
    var daysSinceEnd = (now.Date- _cycleEnd.Date).TotalDays;
    
		UpdateFertilityStatus();
		
		
		if  (_cycleState != CycleState.StopCycle && _cycleState != CycleState.Pregnant && _cycleState != CycleState.Postpartum)
    {
			if (daysSinceEnd >= 0)
    	{
				// Cycle has ended, restart it
        RestartCycle();
    	}
    	else if (daysSinceStart >= 0 && daysSinceStart < MenstrualLength)
    	{
				// Menstrual phase
        CycleState = CycleState.Menstrual;
    	}
			else if (daysSinceStart >= MenstrualLength && daysSinceStart < MenstrualLength + FollicularLength)
			{
				// Follicular phase
        CycleState = CycleState.Follicular;
    	}
    	else if (daysSinceStart >= MenstrualLength + FollicularLength && daysSinceStart < _ovulationDate)
			{
					// Pre-ovulatory phase
        CycleState = CycleState.PreOvulatory;
    	}
    	else if (daysSinceStart >= _ovulationDate && daysSinceStart < _ovulationDate + OvulationLength)
			{
				// Ovulatory phase
        CycleState = CycleState.Ovulation;
    	}
		
		}
		else if (_cycleState == CycleState.Pregant or _cycleState == CycleState.Postpartum)
		{
			UpdatePregnancyStatus();
		}
	}
	
	private void UpdatePregnancyStatus()
	{ 
   	DateTime now = DateTime.Now;
    TimeSpan timeSincePregnancyStart.Date = now - PregnancyStart.DateDate;

		if (_pregnancyState == PregnancyState.IsPregnant && _pregnanceyState != PregnancyState.BlockedPregnancy)
    {
			// Check if pregnancy has already ended
      if (now.Date>= PregnancyEnd.DateDate)
      {
				// Check if postpartum period has ended
				if (now.Date> PregnancyEnd.DateDate.AddDays(PostpartumPeriodLength))
				{
					RestartCycle();
        }
        else if (now.Date>= PregnancyEnd.DateDate.Date && now <= PregnancyEnd.DateDate.AddDays(PostpartumPeriodLength))
        {
        	_cycleState = CycleState.Postpartum;
        }
				else if (timeSincePregnancyStart.Date < TimeSpan.FromDays(7 * 4))
        {
        		// First month of pregnancy
          	PregnancyState = PregnancyState.IsPregnant;
       	}
				else if (timeSincePregnancyStart.Date < TimeSpan.FromDays(7 * 8))
        {
        	// Second month of pregnancy
        	PregnancyState = PregnancyState.MorningSickness;
       	}
				else if (timeSincePregnancyStart.Date >= TimeSpan.FromDays(7 * 12))
				{
					// Third month of pregnancy
					PregnancyState = PregnancyState.IsPregnant;
      	}
			}
		}
	}

	private void UpdateFertilityStatus()
	{
		if  (_cycleState != CycleState.StopCycle && _cycleState != CycleState.Pregnant && _cycleState != CycleState.Postpartum)
    		if (_cycleState == CycleState.Follicular)
    		{
        		if (DayOfCycle >= _fertileWindowStart.Date.Date() && DayOfCycle <= _fertileWindowEnd.Date.Date)
        		{
            		FertilityStatus = FertilityState.Fertile;
        		}
						else
        		{
            		FertilityStatus = FertilityState.Infertile;
        		}
    		}
    		else if (_cycleStatus == CycleState.Luteal)
    		{
        		FertilityStatus = FertilityState.Infertile;
    		}
		}
    	else if(_pregnancyStatus == PregnancyState.IsPregnant || _cycleStatus == CycleState.Pregnant || _pregnancyStatus == PregnancyState.Postpartum)
    	{
       	FertilityStatus = FertilityState.Infertile;
    	}
	}


	public bool CheckBirth()
	{
		now = DateTime.now();
		if ((PregnancyEnd.DateTime - now).Days == 0)
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
		_pregnancyState = PregnancyState.PregnancyBlock; 
	}

	public void StopCycle())
	{
		_cycleState = CycleState.StopCycle;
		_pregnancyState = PregnancyState.BlockPregnancy;	
	}

	public void UnblockCycle()
	{
		_pregnancyState = PregnancyState.NotPregnant;
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
      _CycleState = CycleState.Pregnant;
      _pregnancyStart.Date = DateTime.now();
     PregnancyEnd.Date= _pregnancyStart.Date.AddDays(RandomPregnancyLength());
    }
	}

	public bool IsPregnant()
	{
    	if (_pregnancyState == PregnancyState.IsPregnant)
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
		if (_pregnancyState == PregnancyState.MorningSickness)
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
}